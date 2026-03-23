#ifndef CSERVEUR_WEB_H
#define CSERVEUR_WEB_H

#include <string>
#include <map>

class CServeur_Web {
public:
    CServeur_Web();
    virtual ~CServeur_Web();

    void AnalyserRequete(); // Récupère les données d'Apache
    void Demande_Action();  // Envoie l'ordre à l'automate/IHM
    void Demande_IHM();     // Affiche la page de confirmation

private:
    std::map<std::string, std::string> m_parametres;
    void DecoderPayload(std::string data);
};

#endif