using Test.Domain.Dtos;

namespace Test.Application.Interfaces.Services;

public interface IClientInfoService
{
    ClientInfoDto GetClientInfo();
}