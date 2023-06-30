using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SOLIC.Utilitario
{
    public class ConfiguracoesDaAplicacao
    {
        public static string ObterStringConexaoMongo()
        {
            Configuracao configuracao = new Configuracao();
            return configuracao.ConfiguracaoDoArquivoAppSettings["ConnectionStringMongo"];
        }
    }
}
