using CAFEPAY.ArqHex.Collects.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.application.CollectQueryAll
{
    public class CollectQueryAll
    {
        private readonly CollectRepository collectRepository;

        public CollectQueryAll(CollectRepository _collectRepository){
            this.collectRepository = _collectRepository;
        }
        public List<Collect> execute(){
            return this.collectRepository.queryAll();
        }
    }
}