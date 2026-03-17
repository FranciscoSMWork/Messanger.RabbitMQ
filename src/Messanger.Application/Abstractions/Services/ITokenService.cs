using Messanger.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.Application.Abstractions.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
