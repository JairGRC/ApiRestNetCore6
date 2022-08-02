namespace WebApiAutores.Servicios
{
    public interface IServicio
    {
        Guid ObtenerScoped();
        Guid ObtenerSingleton();
        Guid ObtenerTransient();
        void RealizarTarea();
    }
    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScope servicioScope;
        private readonly ServicioSingleton servicioSingleton;

        public ServicioA(ILogger<ServicioA> logger, ServicioTransient servicioTransient,
            ServicioScope servicioScope,ServicioSingleton servicioSingleton)
        {
            this.logger = logger;
            this.servicioTransient = servicioTransient;
            this.servicioScope = servicioScope;
            this.servicioSingleton = servicioSingleton;
        }
        public Guid ObtenerTransient() { return servicioTransient.guid; }
        public Guid ObtenerScoped() { return servicioScope.guid; }
        public Guid ObtenerSingleton() { return servicioSingleton.guid; }

        public void RealizarTarea()
        {
            
        }
    }
    public class ServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea()
        {
            
        }
    }
    public class ServicioTransient
    {
        public Guid guid=Guid.NewGuid();
    }
    public class ServicioScope
    {
        public Guid guid = Guid.NewGuid();  
    }
    public class ServicioSingleton
    {
        public Guid guid=Guid.NewGuid();    
    }
}
