#include <QtGui/QApplication>
#include "mainform.h"
#include "QTcpSocket"
#include "libhelper/helper.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainForm w;
    w.show();
    w.CONSOLE_Print("[GPROXY] Starting up","cyan");
    w.CONSOLE_Print("[GPROXY] loading config file [gproxy.cfg]","cyan");
    w.CONSOLE_Print("[INFO] Eimai theos!!! To prwto mou pragma sto qt!!","red");
    w.PrintChannelTalk("Damianakos","Eimai cool");
    w.AddUserInChannel("Damianakos");
    w.PrintChannelEmote("Damianakos","Emote!!!!!");
    w.PrintChannelWhisper("GHost_Kamena","Creating public game ggsdemso");
    w.AddUserInChannel("CGamer");
    return a.exec();
}
