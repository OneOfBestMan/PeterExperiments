using AutoMapper;
using AutoMapperLib1;
using AutoMapperLib2;
using StructureMap;
using System;

public partial class _Default : System.Web.UI.Page
{
    Container container;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            container = new Container(init =>
            {
                init.AddRegistry<ConfigurationRegistry>();
            });
        }
        InitWeb();

    }

    public void InitWeb()
    {
        
        var engine = container.GetInstance<IMapper>();

        var source1 = new SourceLib1() { SourceValue = "s1", OtherValue = "haha", Inner = new DestinationInner1() { OtherValue = "hahahha" } };
        var source2 = new SourceLib2() { SourceValue = "s2", OtherValue = "hehe" };
        var dest1 = engine.Map<SourceLib1, DestinationLib1>(source1);
        var dest2 = engine.Map<SourceLib2, DestinationLib2>(source2);
        var dest11 = engine.Map<SourceLib1, DestinationLib1>(source1);
        var dest22 = engine.Map<SourceLib2, DestinationLib2>(source2);
        Response.Write(dest1.SourceValue + " " + dest1.OtherNS + "<br/>");
        Response.Write(dest2.SourceValue + " " + dest2.OtherNS + "<br/>");
        Response.Write(dest1.Inner == null);


        Response.Write(dest11.SourceValue + " " + dest11.OtherNS + "<br/>");
        Response.Write(dest22.SourceValue + " " + dest22.OtherNS + "<br/>");
        Response.Write(dest22.Inner == null);


    }


    public class ConfigurationRegistry : Registry
    {
        public ConfigurationRegistry()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.AddProfile<Lib1Profile>();
                cfg.AddProfile<Lib2Profile>();
            };

            var configuration = new MapperConfiguration(action);
            //Mapper.Initialize(action);
            For<IMapper>().Use(ctx => new Mapper(configuration,ctx.GetInstance));
        }
    }
}