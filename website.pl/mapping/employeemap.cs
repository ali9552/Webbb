using AutoMapper;
using website.dal.entites;
using website.pl.dtos;
using website.pl.dtos.employeedtos;

namespace website.pl.mapping
{
    public class employeemap : Profile
    {
        public employeemap()
        {
            CreateMap<employee, editdtoemployee>();
            CreateMap<editdtoemployee, employee>();
            CreateMap<employee, detailsemployeedto>();
            CreateMap<detailsemployeedto, employee>();
            CreateMap<employee, createemployee>();
            CreateMap<createemployee, employee>();

        }
    }
    


    }

