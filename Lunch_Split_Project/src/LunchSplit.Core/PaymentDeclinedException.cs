using System.Security.Cryptography.X509Certificates;
namespace System;
public class PaymentDeclinedException : Exception
{
  public PaymentDeclinedException(string message):base(message)
  {
      //Do nothing else
  } 
}