// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.ServiceBus.Services.MessageDeferral
{
    using log4net;

    public class MessageDeferralService :
        IHostedService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof (MessageDeferralService));
        private readonly IServiceBus _bus;

        public MessageDeferralService(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Dispose()
        {
        }

        public void Start()
        {
            if (_log.IsInfoEnabled)
                _log.Info("MessageDeferralService Starting");

            _bus.Subscribe<DeferMessageConsumer>();
            _bus.Subscribe<TimeoutExpiredConsumer>();

            if (_log.IsInfoEnabled)
                _log.Info("MessageDeferralService Started");
        }

        public void Stop()
        {
            if (_log.IsInfoEnabled)
                _log.Info("MessageDeferralService Stopping");

            _bus.Unsubscribe<TimeoutExpiredConsumer>();
            _bus.Unsubscribe<DeferMessageConsumer>();

            if (_log.IsInfoEnabled)
                _log.Info("MessageDeferralService Stopped");
        }
    }
}