#include "mainform.h"
#include "ui_mainform.h"
#include <QTime>

MainForm::MainForm(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainForm)
{
    ui->setupUi(this);
}

MainForm::~MainForm()
{
    delete ui;
}

void MainForm::changeEvent(QEvent *e)
{
    QMainWindow::changeEvent(e);
    switch (e->type()) {
    case QEvent::LanguageChange:
        ui->retranslateUi(this);
        break;
    default:
        break;
    }
}
void MainForm::CONSOLE_Print(const QString &message,const QString color )
{
    ui->txtConsole->append("<font color="+color +">" + message+"</font>");
}
void MainForm::PrintChannelTalk(const QString &user,const QString &text)
{
    ui->txtBnet->append("<font color = gold>" + user +": </font><font color = white >" +text +"</font>"   );
}
void MainForm::PrintChannelWhisper(const QString &user, const QString &text)
{
    ui->txtBnet->append("<font color = gold>" + user +" whispers: </font><font color = lime >" +text +"</font>"   );
}
void MainForm::PrintChannelEmote(const QString &user, const QString &text)
{
    ui->txtBnet->append("<font color = gold>" + user +": </font><font color = gray >" +text +"</font>"   );
}
void MainForm::PrintError(const QString &error)
{
     ui->txtBnet->append("<font color = cyan >" +error +"</font>");
}
void MainForm::PrintInfo(const QString &info)
{
     ui->txtBnet->append("<font color = red >" +info +"</font>");
}
void MainForm::AddUserInChannel(const QString &user)
{
    ui->ListUsers->addItem(user);
}
void MainForm::DelUserInChannel(const QString &user)
{
    delete ui->ListUsers->findItems(user,Qt::MatchExactly).at(0);
}
void MainForm::ClearUsersInChannel( )
{
    ui->ListUsers->clear();
}
