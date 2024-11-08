global using FluentAssertions;
global using MercySocial.Application.Common.Authentication.PasswordHasher;
global using MercySocial.Application.Users.Commands.CreateRegisterUser;
global using MercySocial.Application.Users.Repository;
global using MercySocial.Domain.Common.Errors;
global using MercySocial.Domain.UserAggregate.ValueObjects;
global using Moq;
global using Xunit;
global using MercySocial.Domain.UserAggregate;
global using FluentValidation.TestHelper;
global using MercySocial.Application.Users.Queries.UserLogin.Queries;
global using Microsoft.Extensions.Configuration;
global using System.IdentityModel.Tokens.Jwt;