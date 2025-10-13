
using System.Collections.Generic;

public interface IObserver
{
   void OnEventRaised(int message, object additionalInformation);
}
