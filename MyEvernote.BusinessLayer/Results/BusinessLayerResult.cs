using System.Collections.Generic;
using MyEvernote.Entities.Messages;

namespace MyEvernote.BusinessLayer.Results
{
    public class BusinessLayerResult<T> where T:class,new ()
    {
        public BusinessLayerResult()
        {
            Errors=new List<ErrorMessageObj>();
        }
        public List<ErrorMessageObj> Errors { get; set; }
        public T Result { get; set; }


        public void AddError(ErrorMessageCode code,string message)
        {
          Errors.Add(new ErrorMessageObj(){Code = code,Message = message});
        }

    }
}
