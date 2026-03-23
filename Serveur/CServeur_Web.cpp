#include "CServeur_Web.h"
#include <iostream>
#include <fstream>
#include <cstdlib>

CServeur_Web::CServeur_Web() {}
CServeur_Web::~CServeur_Web() {}

void CServeur_Web::AnalyserRequete() {
    char* methode = getenv("REQUEST_METHOD");
    if (!methode) return;

    std::string data;
    if (std::string(methode) == "POST") {
        char* lenStr = getenv("CONTENT_LENGTH");
        if (lenStr) {
            int len = atoi(lenStr);
            for (int i = 0; i < len; ++i) data += getchar();
        }
    }
    else { // GET
        char* query = getenv("QUERY_STRING");
        if (query) data = query;
    }
    DecoderPayload(data);
}

void CServeur_Web::DecoderPayload(std::string data) {
    size_t pos = 0;
    while ((pos = data.find("=")) != std::string::npos) {
        std::string key = data.substr(0, pos);
        data.erase(0, pos + 1);
        size_t end = data.find("&");
        std::string val = data.substr(0, end);
        m_parametres[key] = val;
        if (end == std::string::npos) break;
        data.erase(0, end + 1);
    }
}

void CServeur_Web::Demande_Action() {
    // On simule l'appui sur BtnLancer_Click en créant un fichier de commande
    std::ofstream fichier("C:/Temp/machine_cmd.txt");
    if (fichier.is_open()) {
        fichier << "ACTION=LANCER\n";
        fichier << "CYCLES=" << m_parametres["cycles"] << "\n";
        fichier << "TAILLE=" << m_parametres["taille"] << "\n";
        fichier.close();
    }
}

void CServeur_Web::Demande_IHM() {
    // Header HTTP indispensable pour Apache
    std::cout << "Content-type: text/html\r\n\r\n";
    std::cout << "<html><body style='background:#1A2332; color:white; font-family:sans-serif;'>";
    std::cout << "<h2>Commande de lancement transmise</h2>";
    std::cout << "<p>Cycles : " << m_parametres["cycles"] << "</p>";
    std::cout << "</body></html>";
}