#include <wiringPi.h>

// Broche LED - La broche WiringPi 0 est BCM_GPIO 17.
// nous devons utiliser la numérotation BCM au moment de l'initialisation à l'aide de wiringPiSetupSys
// si vous choisissez un autre numéro de broche, utilisez la numérotation BCM, et
// mettez à jour la commande Pages de propriétés - Événements de build - Événement postbuild distant
// qui utilise l'exportation gpio pour la configuration de wiringPiSetupSys
#define	LED	17

int main(void)
{
	wiringPiSetupSys();

	pinMode(LED, OUTPUT);

	while (true)
	{
		digitalWrite(LED, HIGH);  // Activé
		delay(500); // ms
		digitalWrite(LED, LOW);	  // Désactivé
		delay(500);
	}
	return 0;
}