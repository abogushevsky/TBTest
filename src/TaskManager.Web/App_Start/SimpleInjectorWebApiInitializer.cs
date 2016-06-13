using System.Data;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using TaskManager.BusinessLayer;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Filters;
using TaskManager.DataLayer.Common.Interfaces;
using TaskManager.DataLayer.MsSql;
using TaskManager.DataLayer.MsSql.Dto;
using TaskManager.DataLayer.MsSql.Specialized;
using TaskManager.Web.Controllers;
using TaskManager.Web.Models;

namespace TaskManager.Web
{
    /// <summary>
    /// Вспомогательный класс для инициализации контейнера внедрения зависимостей на основе Simple Injector
    /// http://simpleinjector.codeplex.com/documentation
    /// </summary>
    public static class SimpleInjectorWebApiInitializer
    {
        private static Container _container;

        public static void Initialize()
        {
            if (_container != null)
                return;
            _container = new Container();
            _container.Options.AllowOverridingRegistrations = true;

            InitConverters();
            InitRepositories();
            InitEntityServices();

            _container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(_container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));

            _container.Register<AccountController>(() => new AccountController());
            _container.Options.AllowOverridingRegistrations = false;
        }

        private static void InitConverters()
        {
            _container.Register(typeof(IEntityDtoConverter<,>), new[] { Assembly.Load("TaskManager.DataLayer.MsSql") }, Lifestyle.Singleton);
        }

        private static void InitRepositories()
        {
            CrudCommandsBundle userCommandsBundle = new CrudCommandsBundle()
            {
                GetAllCommand = new SqlCommandInfo("GetAllUserInfos", CommandType.StoredProcedure),
                GetByIdCommand = new SqlCommandInfo("GetUserInfoById", CommandType.StoredProcedure),
                CreateCommand = new SqlCommandInfo("CreateUserInfo", CommandType.StoredProcedure),
                UpdateCommand = new SqlCommandInfo("UpdateUserInfo", CommandType.StoredProcedure),
                DeleteCommand = new SqlCommandInfo("DeleteUserInfo", CommandType.StoredProcedure)
            };

            CrudCommandsBundle categoryCommandsBundle = new CrudCommandsBundle()
            {
                GetAllCommand = new SqlCommandInfo("GetAllCategories", CommandType.StoredProcedure),
                GetByIdCommand = new SqlCommandInfo("GetCategoryById", CommandType.StoredProcedure),
                CreateCommand = new SqlCommandInfo("CreateCategory", CommandType.StoredProcedure),
                UpdateCommand = new SqlCommandInfo("UpdateCategory", CommandType.StoredProcedure),
                DeleteCommand = new SqlCommandInfo("DeleteCategory", CommandType.StoredProcedure)
            };

            CrudCommandsBundle taskCommandsBundle = new CrudCommandsBundle()
            {
                GetAllCommand = new SqlCommandInfo("GetAllTasks", CommandType.StoredProcedure),
                GetByIdCommand = new SqlCommandInfo("GeTaskById", CommandType.StoredProcedure),
                CreateCommand = new SqlCommandInfo("CreateTask", CommandType.StoredProcedure),
                UpdateCommand = new SqlCommandInfo("UpdateTask", CommandType.StoredProcedure),
                DeleteCommand = new SqlCommandInfo("DeleteTask", CommandType.StoredProcedure)
            };

            _container.Register<IRepository<UserInfo, string>>(() => new CrudSqlRepository<UserInfo, string, UserInfoDto>(Resolve<IEntityDtoConverter<UserInfo, UserInfoDto>>(), userCommandsBundle));
            _container.Register<IRepository<Category, int>>(() => new CrudSqlRepository<Category, int, CategoryDto>(Resolve<IEntityDtoConverter<Category, CategoryDto>>(), categoryCommandsBundle));
            _container.Register<IRepository<UserTask, int>>(() => new CrudSqlRepository<UserTask, int, UserTaskDto>(Resolve<IEntityDtoConverter<UserTask, UserTaskDto>>(), taskCommandsBundle));

            _container.Register<IFilteredRepository<Category, CategoriesByUserFilter>>(() => new SqlFilteredRepository<Category, CategoriesByUserFilter>(new SqlCommandInfo("GetUserCategories", CommandType.StoredProcedure)));

            _container.Register<IFilteredRepository<UserTask, TasksByUserFilter>>(() => new SqlFilteredRepository<UserTask, TasksByUserFilter>(new SqlCommandInfo("GetUserTasks", CommandType.StoredProcedure)));
            _container.Register<IFilteredRepository<UserTask, TasksByCategoryFilter>>(() => new SqlFilteredRepository<UserTask, TasksByCategoryFilter>(new SqlCommandInfo("GetTasksByCategory", CommandType.StoredProcedure)));
        }

        private static void InitEntityServices()
        {
            _container.Register<IUsersService>(() => new UsersService(Resolve<IRepository<UserInfo, string>>()));
            _container.Register<ICategoriesService>(() => new CategoriesService(
                Resolve<IRepository<Category, int>>(), 
                Resolve<IFilteredRepository<Category, CategoriesByUserFilter>>()));
            _container.Register<ITaskService>(() => new TaskService(
                Resolve<IRepository<UserTask, int>>(), 
                Resolve<IFilteredRepository<UserTask, TasksByUserFilter>>(),
                Resolve<IFilteredRepository<UserTask, TasksByCategoryFilter>>()));
        }

        public static T Resolve<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}