using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArcEasyObjects.ExceptionAEO
{

    public class InvalidModelAEOException : System.Exception,ISerializable
    {
        private static string _mensagem = "Invalid AEO Model.";

        public static string Mensagem
        {
            get { return InvalidModelAEOException._mensagem; }
        }
        
        public InvalidModelAEOException(): base(_mensagem) { }
        public InvalidModelAEOException(string mensagem) : base(_mensagem + mensagem) { }
        public InvalidModelAEOException(string mensagem, System.Exception ex) : base(_mensagem + mensagem, ex) { }
        public InvalidModelAEOException(System.Exception ex) : base(_mensagem, ex) { }

    }
}
