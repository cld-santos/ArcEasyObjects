using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArcEasyObjects.ExceptionAEO
{

    public class KeyFieldNotFoundException : System.Exception, ISerializable
    {
        private static string _mensagem = "Entity Key Field Not Found.";

        public static string Mensagem
        {
            get { return KeyFieldNotFoundException._mensagem; }
        }

        public KeyFieldNotFoundException() : base(_mensagem) { }
        public KeyFieldNotFoundException(string mensagem) : base(_mensagem + mensagem) { }
        public KeyFieldNotFoundException(string mensagem, System.Exception ex) : base(_mensagem + mensagem, ex) { }
        public KeyFieldNotFoundException(System.Exception ex) : base(_mensagem, ex) { }

    }
}
