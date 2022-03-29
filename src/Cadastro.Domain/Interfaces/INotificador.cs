using Cadastro.Domain.Notificacoes;
using System.Collections.Generic;

namespace Cadastro.Domain.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
