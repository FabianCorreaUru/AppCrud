using AppCrudFabian.Models;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace AppCrudFabian.Servicios
{
    public class ServicioApi : IServicioApi
    {
        private static string usuario;
        private static string clave;
        private static string baseurl;
        private static string token;

        public ServicioApi(IConfiguration config) {
            var ApiSettings = config.GetSection("ApiSettings");
            usuario = ApiSettings.GetSection("usuario").Value.ToString();
            clave = ApiSettings.GetSection("clave").Value.ToString();
            baseurl = ApiSettings.GetSection("baseUrl").Value.ToString();         
        }

        public async Task Autenticar()
        {
            Debug.WriteLine("Autenticacion");
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseurl);
            var credenciales = new Credencial() { usuario = usuario, clave = clave };
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Autenticacion/Validar", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(jsonResponse);
            token = resultado.token;
        }
        public async Task<List<Vehiculo>> Lista()
        {
            List<Vehiculo> lista = new List<Vehiculo>();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            cliente.BaseAddress = new Uri(baseurl);
            var response = await cliente.GetAsync("api/Vehiculo/");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Vehiculo>>(jsonResponse);
                lista = resultado;
            }

            return lista;

        }

        public async Task<Vehiculo> Obtener(int IdVehiculo)
        {
            Vehiculo vehiculo = new Vehiculo();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            cliente.BaseAddress = new Uri(baseurl);
            var response = await cliente.GetAsync($"api/Vehiculo/{IdVehiculo}");
            
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Vehiculo>(jsonResponse);
                vehiculo = resultado;
            }
            return vehiculo;
        }

        public async Task<bool> Guardar(Vehiculo objeto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            cliente.BaseAddress = new Uri(baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
                        
            var response = await cliente.PostAsync("api/Vehiculo/",content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(Vehiculo objeto)
        {
            bool respuesta = false;
            int IdVehiculo = objeto.id; 

            await Autenticar();

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            cliente.BaseAddress = new Uri(baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Vehiculo/{IdVehiculo}", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int IdVehiculo)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            cliente.BaseAddress = new Uri(baseurl);
            var response = await cliente.DeleteAsync($"api/Vehiculo/{IdVehiculo}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }
          
    }
}
