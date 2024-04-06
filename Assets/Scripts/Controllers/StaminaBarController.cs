using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
	[SerializeField] Image _slider;
	private void Update()
	{
		if (PlayerController.Instance.Stamina == 0)
		{
			_slider.enabled = false;
			return;
		}

		_slider.enabled = true;

		if(PlayerController.Instance.Stamina <= 20)
			_slider.color = Color.yellow;
		else
			_slider.color = Color.white;

		gameObject.GetComponent<Slider>().value = PlayerController.Instance.Stamina;
	}
}