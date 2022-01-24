using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace UriScheme.Demo
{
    public class MainWindowViewMdel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Params { get; private set; }

        //public string[] Args { get; private set; }
        public ICommand Change { get; private set; }

        public ICommand CleanLog { get; private set; }

        public MainWindowViewMdel()
        {
            Change = new RelayCommand(ChangeText);
            CleanLog = new RelayCommand(() => { Params.Clear(); });
            Params ??= new ObservableCollection<string>();
            //Args = new string[] { GetUrl().Split('?')[1] };
            //var res = Args.GetParam();
            if (App.Args.Length > 0)
            {
                var res = App.Args.GetParam();
                foreach (var re in res)
                {
                    Params.Add($"{re.Key}");
                    Params.Add($"{re.Value}");
                }
            }
        }

        public void NewParams(string[] args)
        {
            if (args.Length > 0)
            {
                Params.Add("以下是从另一个进程发来的参数");
                var res = args.GetParam();
                foreach (var re in res)
                {
                    Params.Add($"{re.Key}");
                    Params.Add($"{re.Value}");
                }
            }
        }

        private void ChangeText()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string GetUrl()
        {
            return
                "https://newtest.authing.cn/oidc/session/end?id_token_hint=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6bnVsbCwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJzdWIiOiI2MWE1YzU1ZmM4OWZmOTEwODMyOTNlNDUiLCJiaXJ0aGRhdGUiOm51bGwsImZhbWlseV9uYW1lIjpudWxsLCJnZW5kZXIiOiJVIiwiZ2l2ZW5fbmFtZSI6bnVsbCwibG9jYWxlIjpudWxsLCJtaWRkbGVfbmFtZSI6bnVsbCwibmFtZSI6bnVsbCwibmlja25hbWUiOm51bGwsInBpY3R1cmUiOiJodHRwczovL2ZpbGVzLmF1dGhpbmcuY28vYXV0aGluZy1jb25zb2xlL2RlZmF1bHQtdXNlci1hdmF0YXIucG5nIiwicHJlZmVycmVkX3VzZXJuYW1lIjpudWxsLCJwcm9maWxlIjpudWxsLCJ1cGRhdGVkX2F0IjoiMjAyMi0wMS0yMVQwMzowNToyOS4yMTJaIiwid2Vic2l0ZSI6bnVsbCwiem9uZWluZm8iOm51bGwsImFkZHJlc3MiOnsiY291bnRyeSI6bnVsbCwicG9zdGFsX2NvZGUiOm51bGwsInJlZ2lvbiI6bnVsbCwiZm9ybWF0dGVkIjpudWxsfSwicGhvbmVfbnVtYmVyIjpudWxsLCJwaG9uZV9udW1iZXJfdmVyaWZpZWQiOmZhbHNlLCJub25jZSI6ImJnSWlmZFlISUtWSyIsImF0X2hhc2giOiJqUUM4a3R5Yk5VWTNILXFQdXJjdFRBIiwiYXVkIjoiNjFjMmQwNGIzNjMyNDI1OTc3NmFmNzg0IiwiZXhwIjoxNjQzOTQzOTQ0LCJpYXQiOjE2NDI3MzQzNDQsImlzcyI6Imh0dHBzOi8vbmV3dGVzdC5hdXRoaW5nLmNuL29pZGMifQ.Y9PkTUdDHylwTjcc-8fvLinEJ2LWj_awvU5VKDYbvj0&post_logout_redirect_uri=https://www.baidu.com";
        }
    }
}
