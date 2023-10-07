

using Application.Dtos.CartDtos;
using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.FileStorages;
using Application.FluentValidations.CartFluentValidations;
using Application.FluentValidations.CategoryFluentValidations;
using Application.Interfaces;
using Application.PersistentDataStorages;
using AutoMapper;
using Domain.Entities;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Security.Claims;

namespace Application.Features.Carts.Commands
{
    public class CreateCartCommand : IRequest<CartCreateDto>
    {
        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        //public string? VisitorId { get; set; }

    }



    public class CreateCartCommandHandle : IRequestHandler<CreateCartCommand, CartCreateDto>
    {
        private readonly ICartRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        private readonly ISessionService _sessionService;
       

        private readonly FileStorage _fileStorage;
        public CreateCartCommandHandle(ICartRepository repository, FileStorage fileStorage, ISessionService sessionService, IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {

            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _productRepository = productRepository;
            _sessionService = sessionService;
            _fileStorage = fileStorage;
        }

        public async Task<CartCreateDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcı giriş yapmışsa UserId'yi Session üzerinden al
            var userId = await _sessionService.GetAsync("UserId");
            var isActive = !string.IsNullOrEmpty(userId);

            
            // Visitor ID'yi File Storage'dan oku
            var visitorIdFromStorage = _fileStorage.LoadData<string>("VisitorId.txt");

           
            var productcontroll = await _repository.GetProductControl(request.ProductId);

            //ziyaretçi tekrardan aynı ürünü eklemek isterse yeniden ziyaretçi id olusturma 
            if (productcontroll != null &&userId==null)
            {
                visitorIdFromStorage = null;
            }
            
            //user yoksa ve ziyaretçi oalrakta daha önce eklenmediyse ürün ziyaretçiid oluştur
            if (userId==null && string.IsNullOrEmpty(visitorIdFromStorage))
            {
                
                visitorIdFromStorage = GenerateVisitorId(); // GenerateVisitorId, rastgele bir kimlik oluşturur.
                _fileStorage.SaveData("VisitorId.txt", visitorIdFromStorage);
            }

            // Bu ziyaretçi kimliğiyle daha önce bir sepet oluşturulmuş mu kontrol et
            var existingCart =  _repository.GetVisitorCart(visitorIdFromStorage);

            CartCreateDto createCartDto = null;

           
      //user girdiyse ve ziyaretçide sepete ürün eklemişse bu sepeti o user kişisine aktar birleştir
             if (!string.IsNullOrEmpty(userId) && existingCart != null)
            {
                // Kullanıcı giriş yapmış ve ziyaretçi sepetleri mevcutsa
                var visitorCarts = await _repository.GetVisitorCarts(visitorIdFromStorage);

                // Ziyaretçi sepetlerini güncelle ve UserId ekleyin ve ziyaretçiid artık temizleriz
                foreach (var visitorCart in visitorCarts)
                {
                    visitorCart.UserId = userId;
                    visitorCart.IsActive = true;
                    visitorCart.VisitorID = null;
                    await _repository.UpdateAsync(visitorCart);
                }

                createCartDto = _mapper.Map<CartCreateDto>(existingCart); // Dolu olanı döndür
            }
            else
            {
                // Ürün daha önce sepete eklenmiş mi kontrol et ziyaretçi olarak eklenmiş mi 
                var existingCartItem = _repository.GetVisitorCart( visitorIdFromStorage);

                //eğer user varsa ilgili sepette o kaydı dön eğer yoksa boş dön
                var existingCartItemUserId = userId != null ? _repository.GetUserIdCart(userId) : null;
                var productcontrol = await _repository.GetProductControl(request.ProductId);
                //ziyaretçi için aynı ürün varsa adeti arttır

                if (existingCartItem != null && productcontrol!=null )
                {
                    // Ürün daha önce eklenmişse, miktarını artır

                    existingCartItem.Quantity += request.Quantity;
                    await _repository.UpdateAsync(existingCartItem);
                    createCartDto = _mapper.Map<CartCreateDto>(existingCartItem);
                }
                //user için aynı ürün varsa adeti arttır
                else if (existingCartItemUserId != null && productcontrol != null)
                {
                    // Ürün daha önce eklenmişse, miktarını artır
                    existingCartItemUserId.Quantity += request.Quantity;
                    await _repository.UpdateAsync(existingCartItemUserId);
                    createCartDto = _mapper.Map<CartCreateDto>(existingCartItemUserId);
                }


                else
                {
                    //userid varsa visitor yoksa
                    if (userId != null) { visitorIdFromStorage = null; }
                    
                    var product = _productRepository.GetById(request.ProductId);

                  //ürün varsa user yoksa muhtemelen visitor vardır visitora ekle userı boşalt
                    if (productcontrol != null &&userId==null)
                    {
                        productcontrol.Quantity += request.Quantity;
                        productcontrol.VisitorID = visitorIdFromStorage;
                        productcontrol.UserId = null;
                        await _repository.UpdateAsync(productcontrol);
                        createCartDto = _mapper.Map<CartCreateDto>(productcontrol);
                    }

                    //ürün varsa visitor yoksa user vardır aynı üründe userı doldur visitoru boşalt
                    else if(productcontrol!=null && visitorIdFromStorage == null)
                    {
                        productcontrol.Quantity += request.Quantity;
                        productcontrol.VisitorID = visitorIdFromStorage;
                        productcontrol.UserId = userId;
                        await _repository.UpdateAsync(productcontrol);
                        createCartDto = _mapper.Map<CartCreateDto>(productcontrol);
                    }

   //eğer en başta user veye sepete ilk ekz bir ürün ekleniyorsa bu kodlar çalışır direk ekleme yapılır
                    else
                    {
                        var cart = new Cart
                        {
                            Quantity = request.Quantity,
                            Price = product.Price,
                            Date = request.Date,
                            ProductImage = product.ImageUrl,
                            ProductId = request.ProductId,
                            UserId = userId,
                            VisitorID = visitorIdFromStorage,
                            IsActive = isActive
                        };

                        cart.Status = true;

                        var createCart = await _repository.AddAsync(cart);

                        createCartDto = _mapper.Map<CartCreateDto>(createCart);
                    }
                   
                }
            }

            return createCartDto;
        }


        private string GenerateVisitorId()
        {
            // Rastgele bir ziyaretçi kimliği oluşturun
            return Guid.NewGuid().ToString();
        }
    }

   
}