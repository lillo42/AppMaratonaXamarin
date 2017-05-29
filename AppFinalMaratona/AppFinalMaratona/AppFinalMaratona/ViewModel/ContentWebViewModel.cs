using AppFinalMaratona.Model;

namespace AppFinalMaratona.ViewModel
{
    public class ContentWebViewModel : BaseViewModel
    {
        public Podcast Podcast { get; set; }

        public ContentWebViewModel(Podcast podcast)
        {
            Podcast = podcast;
        }
    }
}
