using Agava.YandexGames;

namespace Assets.Scripts.YandexSDK
{
    internal class InterstitialAdShower : AdShower
    {
        public override void Show()
        {
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
        }
    }
}