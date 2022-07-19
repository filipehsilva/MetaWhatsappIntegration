namespace MWI.BitrixPortal.Domain.DTO
{
    public class ConnectorRegisterDto
    {
        public string Auth { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Icon Icon { get; private set; }
        public string Placement_Handler { get; private set; }

        public ConnectorRegisterDto(string auth)
        {
            Id = Settings.IdConnector;
            Name = Settings.ConnectorName;
            Icon = new Icon(Settings.IconSvg, Settings.IconSize, Settings.IconColor, Settings.IconPosition);
            Placement_Handler = Settings.IdConnector;
            Auth = auth;
        }
    }

    public class Icon
    {
        public string Data_Image { get; private set; }
        public string Size { get; private set; }
        public string Color { get; private set; }
        public string Position { get; private set; }

        public Icon(string data_Image, string size, string color, string position)
        {
            Data_Image = data_Image;
            Size = size;
            Color = color;
            Position = position;
        }
    }
}
