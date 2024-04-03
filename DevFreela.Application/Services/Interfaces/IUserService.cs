using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces;

public interface IUserService
{
    UserViewModel GetById(int id);
    void Create(NewUserInputModel inputModel);
    void Login(int id, LoginInputModel inputModel);
}