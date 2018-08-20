using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfApplication1.Models;
using WpfApplication1.UnitsOfWork;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // irá usar o navegador Chrome
        private IWebDriver driver;

        // adiciona o executor de código javascript
        private IJavaScriptExecutor js;

        // url que será acessado
        private string url = "https://www.w3schools.com/";

        // pasta onde será colocado os screenshots
        private string screenshotsPasta;
        // contador para incrementar no nome dos screenshots
        int contador = 1;

        // diretório atual em que será gerado o executável
        private string currentDirectory = "";

        private UnitOfWork _unit = new UnitOfWork();

        public MainWindow()
        {
            InitializeComponent();
                    
            currentDirectory = Environment.CurrentDirectory;
      
            // pasta onde será guardado os screenshots
            screenshotsPasta = Path.Combine(currentDirectory, "screenshot");
            // caso a pasta não exista cria ela
            if (!Directory.Exists(screenshotsPasta))
            {
                Directory.CreateDirectory(screenshotsPasta);
            }
        }

        // Método para capturar screenshot da tela
        public void Screenshot(IWebDriver driver, string screenshotsPasta)
        {
            ITakesScreenshot camera = driver as ITakesScreenshot;
            Screenshot foto = camera.GetScreenshot();
            foto.SaveAsFile(screenshotsPasta, ScreenshotImageFormat.Jpeg);

            // caso queira usar a imagem em memória para não precisar salvar ela
            image.Source = ToImage(foto.AsByteArray);

            // caso queira usar a imagem salva na pasta
            /*BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(screenshotsPasta);
            bi.EndInit();
            image.Source = bi;*/
        }

        // transforma um array de bytes em um BitmapImage
        private BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        // captura a imagem com o nome da data de hoje + _Imagem_ + contador
        public void capturaImagem()
        {
            Screenshot(driver, screenshotsPasta + "\\" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_Imagem_" + contador++ + ".jpg");
            Thread.Sleep(500);
        }

        // função que espera a página ser carregada => quando o document estiver no estado ready e completo 
        // para poder ser acessado os elementos html na página
        private void WaitPageLoad()
        {
            // espera x segundos até lançar uma exceção
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        // clique no botão
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //var driver = new InternetExplorerDriver();
            //var driver = new PhantomJSDriver();

            // procura o driver no diretório
            driver = new ChromeDriver(currentDirectory);

            // id do usuario para ser usado quando for salvar as tags
            var usuarioId = 0;
            Usuario usuarioExistente = null;
            try
            {
                // procura se já existe um usuario com essa matricula
                usuarioExistente = _unit.UsuarioRepository.SearchFor(u => u.Matricula == textBox.Text).FirstOrDefault();
                // se exisitir apenas atualiza a data de acesso
                if (usuarioExistente != null)
                {
                    usuarioExistente.DataAcesso = DateTime.Now;
                    _unit.UsuarioRepository.Update(usuarioExistente);
                    usuarioId = usuarioExistente.UsuarioId;
                    _unit.Save();
                }
                // senão grava o novo usuario
                else
                {
                    // model Usuario
                    Usuario usuario = new Usuario();
                    // valores vindos dos campos de input do formulário
                    usuario.Matricula = textBox.Text;
                    usuario.Senha = passwordBox.Password;
                    usuario.CampoNaoNulo = 3;
                    usuario.DataCriacao = DateTime.Now;
                    usuario.DataAcesso = DateTime.Now;
                    usuarioExistente = _unit.UsuarioRepository.Add(usuario);
                    _unit.Save();
                    // pega o id do usuario salvo
                    usuarioId = usuario.UsuarioId;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // acessa a página inicial
            paginaInicial();

            // acessa a página de selects
            selectPage();

            // acessa a página html e o link html input types
            paginaHtmlInputTypes(usuarioId);

            // acessa o link CSS
            paginaCss(usuarioId);

            // fecha tudo
            driver.Quit();
        }

        // função para executar a função js para adicionar borda vermelha aos elementos
        private void runJS(IReadOnlyCollection<IWebElement> elements)
        {
            // itera por todos os elementos e adiciona border:1px solid red a cada elemento
            foreach (var element in elements)
            {
                // executa o script
                js.ExecuteScript("arguments[0].setAttribute('style', 'border:1px solid red')", element);
            }
        }

        // função para executar a função js para adicionar borda vermelha ao único elemento
        private void runJS(IWebElement element)
        {
            // itera por todos os elementos e adiciona border:1px solid red a cada elemento
            // executa o script
            js.ExecuteScript("arguments[0].setAttribute('style', 'border:1px solid red')", element);
        }

        private void paginaInicial()
        {
            // acessa a url
            driver.Url = url;
            WaitPageLoad();

            // maximiza a janela
            //driver.Manage().Window.Maximize();

            // inicializa o executor de JS
            js = (IJavaScriptExecutor)driver;

            // pega todos os elementos com a classe w3-center
            var elements = driver.FindElements(By.ClassName("w3-center"));
            // executa o JS
            runJS(elements);

            capturaImagem();
        }

        private void paginaCss(int usuarioId)
        {
            // clica no link com o texto: Learn CSS
            driver.FindElement(By.LinkText("Learn CSS")).Click();
            WaitPageLoad();

            // captura todas as tags h2 da página
            var h2s = driver.FindElements(By.TagName("h2"));
            runJS(h2s);
            capturaImagem();

            // itera por todos os h2s
            foreach (var h2 in h2s)
            {
                // pega o texto da tag
                var text = h2.Text;
                Tag tag = new Tag()
                {
                    Data = DateTime.Now,
                    Name = "h2",
                    Text = text,
                    UsuarioId = usuarioId
                };
                // adiciona todas as tags ao repositorio para ser salvo depois
                _unit.TagRepository.Add(tag);
            }

            try
            {
                _unit.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // volta para página inicial
            driver.Navigate().Back();
        }

        private void paginaHtmlInputTypes(int usuarioId)
        {
            // clica no link com o texto: Learn HTML
            driver.FindElement(By.LinkText("Learn HTML")).Click();
            WaitPageLoad();
            capturaImagem();

            // clica no link com o texto: HTML Input Types
            driver.FindElement(By.LinkText("HTML Input Types")).Click();
            WaitPageLoad();

            // pega o radio button através do seletor css tipo - valor
            var radio1 = driver.FindElement(By.CssSelector("input[type = 'radio'][value = 'V2']"));
            radio1.Click();

            Tag tag1 = new Tag()
            {
                Data = DateTime.Now,
                UsuarioId = usuarioId,
                Name = radio1.GetAttribute("type"),
                Text = radio1.GetAttribute("value")
            };
            _unit.TagRepository.Add(tag1);

            // pega o radio button através do xpath name e valor
            var radio2 = driver.FindElement(By.XPath("//input[@name='gender' and @value='V3']"));
            radio2.Click();

            Tag tag2 = new Tag()
            {
                Data = DateTime.Now,
                UsuarioId = usuarioId,
                Name = radio2.GetAttribute("type"),
                Text = radio2.GetAttribute("value")
            };
            _unit.TagRepository.Add(tag2);

            // pega o checkboox através do name
            var checkBox = driver.FindElement(By.Name("vehicle2"));
            checkBox.Click();

            Tag tag3 = new Tag()
            {
                Data = DateTime.Now,
                UsuarioId = usuarioId,
                Name = checkBox.GetAttribute("type"),
                Text = checkBox.GetAttribute("value")
            };
            _unit.TagRepository.Add(tag3);

            // salva os dados
            try
            {
                _unit.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // volta para página inicial
            driver.Navigate().Back();
            driver.Navigate().Back();
        }

        private void selectPage()
        {
            // vai para a página abaixo
            driver.Url = "https://www.w3schools.com/html/tryit.asp?filename=tryhtml_elem_select_size";
            WaitPageLoad();
            capturaImagem();

            // muda para o frame com name="iframeResult"
            driver.SwitchTo().Frame("iframeResult");
            // pega o elemento select com name="cars"
            var selectElement = driver.FindElement(By.Name("cars"));
            // cria um select element do support.ui
            var select = new SelectElement(selectElement);
            // selecione o select pelo value
            select.SelectByValue("saab");

            // volta para a página inicial
            driver.Navigate().Back();
        }
    }
}
