using Cadastro.Domain.Validations.Dados;
using System;
using System.Text;

namespace Cadastro.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected string Encode(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        protected string Decode(string password)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(password));
        }
    }
}

