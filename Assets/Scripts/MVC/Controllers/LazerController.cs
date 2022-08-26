using UnityEngine;

namespace Asteroids
{
    public class SpawnLazerController : BaseController
    {
        private SpawnLazerView _lazerView;

        public Lazer CurrentLazer { get; }

        private float _currentBatteryLifeTime;
        private bool _lazerIsActive = true;
        private bool _isNextBattery = false;

        private AsteroidsInputSystem _inputSystem;

        public SpawnLazerController(SpawnLazerView view, AsteroidsInputSystem inputSystem)
        {
            _lazerView = view;
            _inputSystem = inputSystem;
            CurrentLazer = new Lazer(_lazerView.LazerTime, _lazerView.Lazer, _lazerView.CountBattery,
                _lazerView.BatteryLifeTime, _lazerView.BatteryReloadTime);

            CurrentLazer.CurrentCountBattery = CurrentLazer.CountBattery;
            _currentBatteryLifeTime = CurrentLazer.BatteryLifeTime;
            CurrentLazer.CurrentBatteryReloadTime = CurrentLazer.BatteryReloadTime;
        }

        public override void Tick()
        {
            
            if (_inputSystem.Player.Lazer.IsPressed() && !CurrentLazer.LazerTransform.gameObject.activeSelf)
            {
               
                if (_lazerIsActive)
                {
                    _isNextBattery = true;
                    CurrentLazer.CurrentBatteryReloadTime = CurrentLazer.BatteryReloadTime;
                    CurrentLazer.LazerTransform.gameObject.SetActive(true);
                }
                else
                {
                    CurrentLazer.LazerTransform.gameObject.SetActive(false);
                    _isNextBattery = false;
                }
            }


            if (!_inputSystem.Player.Lazer.IsPressed() && CurrentLazer.LazerTransform.gameObject.activeSelf)
            {
                CurrentLazer.LazerTransform.gameObject.SetActive(false);
                _isNextBattery = false;
            }

            if (_isNextBattery && _lazerIsActive)
            {
                _currentBatteryLifeTime -= 0.01f;
            }

            if (_currentBatteryLifeTime < 0 && _lazerIsActive)
            {
                CurrentLazer.CurrentCountBattery--;
                _currentBatteryLifeTime = CurrentLazer.BatteryLifeTime;
            }

            if (!_isNextBattery && CurrentLazer.CurrentBatteryReloadTime >= 0 &&
                CurrentLazer.CurrentCountBattery < CurrentLazer.CountBattery)
            {
                CurrentLazer.CurrentBatteryReloadTime -= 0.01f;
            }

            if (!_isNextBattery && CurrentLazer.CurrentBatteryReloadTime <= 0 &&
                CurrentLazer.CurrentCountBattery < CurrentLazer.CountBattery)
            {
                CurrentLazer.CurrentCountBattery++;
                CurrentLazer.CurrentBatteryReloadTime = CurrentLazer.BatteryReloadTime;
            }

            _lazerIsActive = CurrentLazer.CurrentCountBattery > 0;
            
        }

    }
}
