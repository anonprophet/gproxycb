#ifndef MAINFORM_H
#define MAINFORM_H

#include <QMainWindow>

namespace Ui {
    class MainForm;
}

class MainForm : public QMainWindow {
    Q_OBJECT
public:
    MainForm(QWidget *parent = 0);
    ~MainForm();
    void CONSOLE_Print(const QString &message,const QString color );
    void PrintChannelTalk( const QString &user, const QString &text);
    void PrintChannelWhisper( const QString &user, const QString &text);
    void PrintChannelEmote( const QString &user, const QString &text);
    void PrintInfo(const QString &info);
    void PrintError( const QString &error);
    void AddUserInChannel( const QString &user );
    void DelUserInChannel( const QString &user);
    void ClearUsersInChannel( );

protected:
    void changeEvent(QEvent *e);

private:
    Ui::MainForm *ui;

};

#endif // MAINFORM_H
