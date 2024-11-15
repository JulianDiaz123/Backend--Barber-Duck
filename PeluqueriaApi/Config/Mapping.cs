using AutoMapper;
using PeluqueriaApi.Models.Producto;
using PeluqueriaApi.Models.Producto.Dto;
using PeluqueriaApi.Models.Servicio;
using PeluqueriaApi.Models.Servicio.Dto;
using PeluqueriaApi.Models.Turno;
using PeluqueriaApi.Models.Turno.Dto;
using PeluqueriaApi.Models.User;
using PeluqueriaApi.Models.User.Dto;

namespace PeluqueriaApi.Config
{
    public class Mapping : Profile
    {
        public Mapping() 
        { 
            // Para no convertir los atributos 'int?' a 0 en la conversion de los 'null'
            // valor defecto int -> 0
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            // Aqui es necesario hacer esto con bool? ya que tampoco devuelve el tipo 'null'.
            // valor defecto bool -> false
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);


            // Productos
            //PD: Esta solución hay que aplicarla para todos aquellos tipos que no tengan como valor por defecto 'null'
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Producto, ProductosDTO>().ReverseMap();
            CreateMap<Producto, CreateProductoDTO>().ReverseMap();

            // Actualizar y no parsear los valores 'NULL'
            CreateMap<UpdateProductoDTO, Producto>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });


            // Servicios
            CreateMap<Servicio, ServicioDTO>().ReverseMap();
            CreateMap<Servicio, ServiciosDTO>().ReverseMap();
            CreateMap<Servicio, CreateServicioDTO>().ReverseMap();

            CreateMap<UpdateServicioDTO, Servicio>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });


            // Turnos
            CreateMap<Turno, TurnoDTO>().ReverseMap();
            CreateMap<Turno, TurnosDTO>().ReverseMap();
            CreateMap<Turno, CreateTurnoDTO>().ReverseMap();

            CreateMap<UpdateTurnoDTO, Turno>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            // Usuarios
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UsersDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();

            CreateMap<UpdateUserDTO, User>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            // Roles
            CreateMap<User, UserLoginResponseDTO>().ForMember(
                dest => dest.Roles,
                opt => opt.MapFrom(src => src.Roles.Select(r => r.Name).ToList())
            );
        }
    }
}
