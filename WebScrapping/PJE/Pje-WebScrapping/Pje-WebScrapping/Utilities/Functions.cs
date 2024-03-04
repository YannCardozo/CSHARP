using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Utilities
{
    public class Functions
    {
        // Função para encontrar o índice do primeiro caractere numérico na string
        public static int EncontrarIndiceInicioNumero(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                if (char.IsDigit(texto[i]))
                {
                    return i;
                }
            }
            return -1; // Se não houver número na string
        }

        // Função para encontrar o índice do último caractere numérico na string
        public static int EncontrarIndiceFimNumero(string texto, int indiceInicio)
        {
            for (int i = texto.Length - 1; i >= indiceInicio; i--)
            {
                if (char.IsDigit(texto[i]))
                {
                    return i;
                }
            }
            return -1; // Se não houver número na string
        }
    }
}
