using Gladys.DependenciesServices.IServices;
using Gladys.Models.Firms;
using Gladys.Models.JsonModel;
using Gladys.Models.MainPageModel;
using Gladys.Models.TemplateTable;
using Gladys.Services.IServices;
using Gladys.Services.Services;
using Gladys.ViewModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gladys.ViewModel.ViewModels
{
    public class MainPageController : BaseViewModel
    {
        #region Propriétés
        private readonly IGetRessources _getRessources;
        private readonly ILuisService _luisService;
        private IRequestWebService _requestWebService;
        private readonly List<JsonModel> _listJsonModel = new List<JsonModel>();
        public string Test { get; set; } = "Bonjour";

        private ObservableCollection<SpeechModel> _speech = new ObservableCollection<SpeechModel>();
        public ObservableCollection<SpeechModel> Speech
        {
            get { return _speech; }
            set
            {
                _speech = value;
                OnPropertyChanged("Speech");
            }
        }
        private string _request;
        public string Request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged("Request");
            }
        }
        private string _placeHolder;
        public string PlaceHolder
        {
            get { return _placeHolder; }
            set
            {
                _placeHolder = value;
                OnPropertyChanged("PlaceHolder");
            }
        }
        public ICommand GetRequest
        {
            get { return new Command(p => GetRequestAsync()); }
        }
        public ICommand PopupFirmCommand
        {
            get { return new Command(async p => await PopupFirm()); }
        }
        public ICommand PopupModelCommand
        {
            get { return new Command(async p => await PopupModel()); }
        }
        public ICommand PopupDataCommand
        {
            get { return new Command(async p => await PopupData()); }
        }

        private List<Firm> _firmsDb = new List<Firm>();
        #region Firm
        private bool _isPopupFirmVisible = false;
        public bool IsPopupFirmVisible
        {
            get { return _isPopupFirmVisible; }
            set
            {
                _isPopupFirmVisible = value;
                OnPropertyChanged("IsPopupFirmVisible");
            }
        }
        private Firm _firm = new Firm();
        public Firm Firm
        {
            get { return _firm; }
            set
            {
                if (_firm != value && !string.IsNullOrEmpty(value.FirmName))
                    Task.Run(async () => await GetModel());
                _firm = value;
                if (!string.IsNullOrEmpty(value.FirmName))
                    Task.Run(async () => await PopupFirm());
                OnPropertyChanged("Firm");
                Request = string.Empty;
                PlaceHolder = "Référence";
            }
        }

        private List<Firm> _firms = new List<Firm>();
        public List<Firm> Firms
        {
            get { return _firms; }
            set
            {
                _firms = value;
                OnPropertyChanged("Firms");
            }
        }
        #endregion

        #region model

        private bool _isPopupModelVisible = false;
        public bool IsPopupModelVisible
        {
            get { return _isPopupModelVisible; }
            set
            {
                _isPopupModelVisible = value;
                OnPropertyChanged("IsPopupModelVisible");
            }
        }

        private List<Firm> _firmSelected = new List<Firm>();
        public List<Firm> FirmSelected
        {
            get { return _firmSelected; }
            set
            {
                _firmSelected = value;
                OnPropertyChanged("FirmSelected");
            }
        }
        private Firm _modelSelected = new Firm();
        public Firm ModelSelected
        {
            get { return _modelSelected; }
            set
            {
                if (_modelSelected != value && !string.IsNullOrEmpty(value.FirmName))
                    Task.Run(async () => await GetTemplate());
                _modelSelected = value;
                if (!string.IsNullOrEmpty(value.FirmName))
                    Task.Run(async () => await PopupModel());
                OnPropertyChanged("ModelSelected");
                Request = string.Empty;
                PlaceHolder = "Erreurs";
            }
        }
        #endregion
        #region Datas
        private bool _isPopupDataVisible = false;
        public bool IsPopupDataVisible
        {
            get { return _isPopupDataVisible; }
            set
            {
                _isPopupDataVisible = value;
                OnPropertyChanged("IsPopupDataVisible");
            }
        }

        private List<TemplateTableModel> _datasDB = new List<TemplateTableModel>();
        private List<TemplateTableModel> _datas = new List<TemplateTableModel>();
        public List<TemplateTableModel> Datas
        {
            get { return _datas; }
            set
            {
                _datas = value;
                OnPropertyChanged("Datas");
            }
        }
        private TemplateTableModel _data = new TemplateTableModel();
        public TemplateTableModel Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                    Task.Run(async () => await GetSolution());
                _data = value;
                if (!string.IsNullOrEmpty(_data.Problem))
                    Task.Run(async () => await PopupData());
                OnPropertyChanged("Data");
                Request = string.Empty;
            }
        }
        private List<TemplateTableModel> _dataSelected = new List<TemplateTableModel>();
        public List<TemplateTableModel> DataSelected
        {
            get { return _dataSelected; }
            set
            {
                _dataSelected = value;
                OnPropertyChanged("DataSelected");
            }
        }
        #endregion
        #region Search
        private bool _isTextEnabled=false;
        public bool IsTextEnabled
        {
            get { return _isTextEnabled; }
            set
            {
                _isTextEnabled = value;
                OnPropertyChanged("IsTextEnabled");
            }
        }
        private bool _isRotaionEntry;
        public bool IsRotaionEntry
        {
            get { return _isRotaionEntry; }
            set
            {
                _isRotaionEntry = value;
                OnPropertyChanged("IsRotaionEntry");
            }
        }
        

        #endregion
        #endregion

        #region CTR
        public MainPageController()
        {
            _luisService = new LuisService();
            _getRessources = new GetRessources();
            _requestWebService = new RequestWebService();
            _listJsonModel = GetDialogues();
            Device.BeginInvokeOnMainThread(async () => Speech = await AddSpeech("Bonjour, que cherchez vous?"));

            Device.BeginInvokeOnMainThread(async () => await GetFirms());
            
        }

        private async Task GetFirms(string namefirm = null)
        {
            IFirmServices fs = new FirmServices();

            if (namefirm != null)
            {
                _firmsDb = (await fs.GetFirms()).Where(f => f.FirmName.ToLower().Contains(namefirm.ToLower())).ToList();
            }
            else
                _firmsDb = await fs.GetFirms();

            var result = (_firmsDb.GroupBy(f => f.FirmName).Select(fSelect => new Firm { FirmName = fSelect.Key })).ToList();
            Firms = result;
        }
        #endregion
        /// <summary>
        /// Appel le service de réponse
        /// </summary>
        private async void GetRequestAsync()
        {
            EntryAnim();
            //LuisResult reponseLuis = await _luisService.SendRequest("salut");

            // Je vérifie que l'utilisateur n'a pas tapé de texte
            //if (String.IsNullOrEmpty(Request))
            //{
            //    // Reconnaissance des paroles
            //    Request = await DependencyService.Get<ISpeechToText>().Reconnaissance();
            //}

            //Appel un service
            try
            {
                JsonModel test = new JsonModel();
                var result = await _requestWebService.RequestAsync(Request);
                JsonConvert.DeserializeObject<JsonModel>(result);
            }
            catch (Exception ex)
            {
                LogService.WriteLog(ex);
            }


            if (!IsTextEnabled)
                return;
            if (String.IsNullOrEmpty(Request))
                return;
            // Reponse de Gladys
            //await DependencyService.Get<ITextToSpeech>().SpeakAsync("Bonjour, Fabien");
            //await DependencyService.Get<ITextToSpeech>().SpeakAsync(Request);
            Device.BeginInvokeOnMainThread(async () => Speech = await AddSpeech());
            if (String.IsNullOrEmpty(Firm.FirmName))
            {
                await GetFirms(Request);
                IsPopupFirmVisible = true;
            }
            else if (String.IsNullOrEmpty(ModelSelected.FirmName))
            {
                await GetModel(Request);
                IsPopupModelVisible = true;
            }
            //reponse de Gladys
            //Device.BeginInvokeOnMainThread(async () => Speech = await AddSpeech(Request));
        }

        private void EntryAnim()
        {
            if (IsTextEnabled)
            { IsRotaionEntry = !_isRotaionEntry;
                
            }
            if (!IsTextEnabled)
            { IsTextEnabled = true;
                PlaceHolder = "Société";
            }

        }
        /// <summary>
        /// Récupère les dialogues du fichier json
        /// </summary>
        /// <returns></returns>
        private List<JsonModel> GetDialogues()
        {
            var retour = _getRessources.GetStream("Gladys.Services.Json.responses.json");
            using (var reader = new System.IO.StreamReader(retour))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<JsonModel>>(json);
            }
        }
        /// <summary>
        /// Add dialogue de l'utilisateur
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<SpeechModel>> AddSpeech()
        {
            var listSpeech = Speech.ToList();
            listSpeech.Add(new SpeechModel { Text = Request, IsUser = true, Color = "#f8c291", Date = DateTime.Now, Align = TextAlignment.Start, Horizontal = LayoutOptions.Start, Index = 0 });
            ObservableCollection<SpeechModel> collect = new ObservableCollection<SpeechModel>(listSpeech.OrderBy(d => d.Date));
            return collect;
        }
        /// <summary>
        /// Add dialogue de Gladys
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<SpeechModel>> AddSpeech(string message)
        {
            var listSpeech = Speech.ToList();
            await DependencyService.Get<ITextToSpeech>().SpeakAsync(message);
            listSpeech.Add(new SpeechModel { Text = message, IsUser = false, Color = "#00ccff", Date = DateTime.Now, Align = TextAlignment.End, Horizontal = LayoutOptions.EndAndExpand, Index = 1 });
            ObservableCollection<SpeechModel> collect = new ObservableCollection<SpeechModel>(listSpeech.OrderBy(d => d.Date));
            return collect;
        }

        private async Task GetModel(string modelname = null)
        {
            if (true)
            {
                Device.BeginInvokeOnMainThread(async () => FirmSelected = _firmsDb.Where(n => n.FirmName == Firm.FirmName).ToList());
                //.ToLower()
            }
            Device.BeginInvokeOnMainThread(async () => FirmSelected = _firmsDb.Where(n => n.FirmName == Firm.FirmName).ToList());
        }
        /// <summary>
        /// Get Support
        /// </summary>
        /// <returns></returns>
        private async Task GetTemplate()
        {
            IFirmDatasServices firmDatasServices = new FirmDatasServices();
            _datasDB = await firmDatasServices.GetDatas(ModelSelected.TableName);
            Device.BeginInvokeOnMainThread(() => Datas = _datasDB.Where(c => c.OrderSolution == 1).ToList());
        }
        /// <summary>
        /// Get solution
        /// </summary>
        /// <returns></returns>
        private async Task GetSolution()
        {
            DataSelected = _datasDB.Where(s => s.Classification == Data.Classification).ToList();
            string solution = string.Empty;
            foreach (var dataSelect in DataSelected)
            {
                solution = solution + " . " + dataSelect.Solution;
                //Device.BeginInvokeOnMainThread(async () => await DependencyService.Get<ITextToSpeech>().SpeakAsync(dataSelect.Solution));
                //break;
            }

            Device.BeginInvokeOnMainThread(async () => await DependencyService.Get<ITextToSpeech>().SpeakAsync(solution));
        }
        /// <summary>
        /// Display popup firm
        /// </summary>
        private async Task PopupFirm()
        {
            if (IsPopupFirmVisible == true)
            {
                IsPopupFirmVisible = false;
            }
            else
                IsPopupFirmVisible = true;
            //IsPopupFirmVisible = !_isPopupFirmVisible;
            if (!IsPopupFirmVisible)
                Device.BeginInvokeOnMainThread(async () => Speech = await AddSpeech("Vous avez choisi " + Firm.FirmName));
        }

        /// <summary>
        /// Display popup firm
        /// </summary>
        private async Task PopupModel()
        {
            if (IsPopupModelVisible == true)
            {
                IsPopupModelVisible = false;
            }
            else
                IsPopupModelVisible = true;
            //IsPopupFirmVisible = !_isPopupFirmVisible;
            if (!IsPopupModelVisible)
                Device.BeginInvokeOnMainThread(async () => Speech = await AddSpeech("Vous avez choisi " + ModelSelected.ModelName));
        }
        /// <summary>
        /// Display popup firm
        /// </summary>
        private async Task PopupData()
        {
            if (IsPopupDataVisible == true)
            {
                IsPopupDataVisible = false;
            }
            else
                IsPopupDataVisible = true;
            //IsPopupFirmVisible = !_isPopupFirmVisible;
            if (!IsPopupDataVisible)
                Device.BeginInvokeOnMainThread(async () => Speech = await AddSpeech("Vous avez choisi " + Data.Problem));
        }
    }
}
