﻿using System.Data;
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
        private const string CONNECTION_STRING_NAME = "DefaultConnection";

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

            _container.Register<AccountController>(() => new AccountController(Resolve<IUsersService>()));
            _container.Options.AllowOverridingRegistrations = false;
        }

        private static void InitConverters()
        {
            _container.Register(typeof(IEntityDtoConverter<,>), new[] { Assembly.Load("TaskManager.DataLayer.MsSql") }, Lifestyle.Singleton);
        }

        private static void InitRepositories()
        {
            //CrudCommandsBundle userCommandsBundle = new CrudCommandsBundle()
            //{
            //    GetAllCommand = new SqlCommandInfo("GetAllUserInfos", CommandType.StoredProcedure),
            //    GetByIdCommand = new SqlCommandInfo("GetUserInfoById", CommandType.StoredProcedure),
            //    CreateCommand = new SqlCommandInfo("CreateUserInfo", CommandType.StoredProcedure),
            //    UpdateCommand = new SqlCommandInfo("UpdateUserInfo", CommandType.StoredProcedure),
            //    DeleteCommand = new SqlCommandInfo("DeleteUserInfo", CommandType.StoredProcedure)
            //};

            CrudCommandsBundle categoryCommandsBundle = new CrudCommandsBundle()
            {
                GetAllCommand = new SqlCommandInfo("sp_GetAllCategories", CommandType.StoredProcedure),
                GetByIdCommand = new SqlCommandInfo("sp_GetCategoryById", CommandType.StoredProcedure),
                CreateCommand = new SqlCommandInfo("sp_CreateCategory", CommandType.StoredProcedure),
                UpdateCommand = new SqlCommandInfo("sp_UpdateCategory", CommandType.StoredProcedure),
                DeleteCommand = new SqlCommandInfo("sp_DeleteCategory", CommandType.StoredProcedure)
            };

            CrudCommandsBundle taskCommandsBundle = new CrudCommandsBundle()
            {
                GetAllCommand = new SqlCommandInfo("sp_GetAllTasks", CommandType.StoredProcedure),
                GetByIdCommand = new SqlCommandInfo("sp_GeTaskById", CommandType.StoredProcedure),
                CreateCommand = new SqlCommandInfo("sp_CreateTask", CommandType.StoredProcedure),
                UpdateCommand = new SqlCommandInfo("sp_UpdateTask", CommandType.StoredProcedure),
                DeleteCommand = new SqlCommandInfo("sp_DeleteTask", CommandType.StoredProcedure)
            };

            //_container.Register<IRepository<UserInfo, string>>(() => new CrudSqlRepository<UserInfo, string, UserInfoDto>(
            //    Resolve<IEntityDtoConverter<UserInfo, UserInfoDto>>(), 
            //    userCommandsBundle, 
            //    CONNECTION_STRING_NAME));
            _container.Register<IRepository<Category, int>>(() => new CrudSqlRepository<Category, int, CategoryDto>(
                Resolve<IEntityDtoConverter<Category, CategoryDto>>(), 
                categoryCommandsBundle, 
                CONNECTION_STRING_NAME));
            _container.Register<IRepository<UserTask, int>>(() => new CrudSqlRepository<UserTask, int, UserTaskDto>(
                Resolve<IEntityDtoConverter<UserTask, UserTaskDto>>(), 
                taskCommandsBundle, 
                CONNECTION_STRING_NAME));

            _container.Register<IFilteredRepository<Category, CategoriesByUserFilter>>(() => new SqlFilteredRepository<Category, CategoriesByUserFilter>(
                new SqlCommandInfo("sp_GetUserCategories", CommandType.StoredProcedure), 
                CONNECTION_STRING_NAME));

            _container.Register<IFilteredRepository<UserTask, TasksByUserFilter>>(() => new SqlFilteredRepository<UserTask, TasksByUserFilter>(
                new SqlCommandInfo("sp_GetUserTasks", CommandType.StoredProcedure),
                CONNECTION_STRING_NAME));
            _container.Register<IFilteredRepository<UserTask, TasksByCategoryFilter>>(() => new SqlFilteredRepository<UserTask, TasksByCategoryFilter>(
                new SqlCommandInfo("sp_GetTasksByCategory", CommandType.StoredProcedure), 
                CONNECTION_STRING_NAME));
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