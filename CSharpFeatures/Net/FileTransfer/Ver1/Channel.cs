namespace J9Updater.FileTransferSvc.Ver1
{
    internal class Channel
    {
        private TransmitConfig transmitConfig;

        public Channel(TransmitConfig transmitConfig)
        {
            this.transmitConfig = transmitConfig;
            this.Receiver = new Receiver(transmitConfig);
            this.Sender = new Sender(transmitConfig);
        }

        public TransmitConfig TransConfig { get; set; }
        public Sender Sender { get; set; }
        public Receiver Receiver { get; set; }
    }
}
