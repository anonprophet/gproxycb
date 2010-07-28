/********************************************************************************
** Form generated from reading UI file 'mainform.ui'
**
** Created: Mon 26. Jul 16:11:12 2010
**      by: Qt User Interface Compiler version 4.6.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINFORM_H
#define UI_MAINFORM_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QGridLayout>
#include <QtGui/QHBoxLayout>
#include <QtGui/QHeaderView>
#include <QtGui/QLineEdit>
#include <QtGui/QListWidget>
#include <QtGui/QMainWindow>
#include <QtGui/QTabWidget>
#include <QtGui/QTextEdit>
#include <QtGui/QVBoxLayout>
#include <QtGui/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainForm
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout;
    QTabWidget *Tabs;
    QWidget *TabConsole;
    QVBoxLayout *verticalLayout_3;
    QVBoxLayout *verticalLayout;
    QTextEdit *txtConsole;
    QLineEdit *LineCommander;
    QWidget *TabBnet;
    QGridLayout *gridLayout_3;
    QHBoxLayout *BnetLayout;
    QVBoxLayout *LineTextLayout;
    QTextEdit *txtBnet;
    QLineEdit *LineBnet;
    QListWidget *ListUsers;

    void setupUi(QMainWindow *MainForm)
    {
        if (MainForm->objectName().isEmpty())
            MainForm->setObjectName(QString::fromUtf8("MainForm"));
        MainForm->resize(643, 351);
        centralWidget = new QWidget(MainForm);
        centralWidget->setObjectName(QString::fromUtf8("centralWidget"));
        gridLayout = new QGridLayout(centralWidget);
        gridLayout->setSpacing(6);
        gridLayout->setContentsMargins(11, 11, 11, 11);
        gridLayout->setObjectName(QString::fromUtf8("gridLayout"));
        Tabs = new QTabWidget(centralWidget);
        Tabs->setObjectName(QString::fromUtf8("Tabs"));
        Tabs->setAutoFillBackground(true);
        TabConsole = new QWidget();
        TabConsole->setObjectName(QString::fromUtf8("TabConsole"));
        verticalLayout_3 = new QVBoxLayout(TabConsole);
        verticalLayout_3->setSpacing(6);
        verticalLayout_3->setContentsMargins(11, 11, 11, 11);
        verticalLayout_3->setObjectName(QString::fromUtf8("verticalLayout_3"));
        verticalLayout = new QVBoxLayout();
        verticalLayout->setSpacing(6);
        verticalLayout->setObjectName(QString::fromUtf8("verticalLayout"));
        txtConsole = new QTextEdit(TabConsole);
        txtConsole->setObjectName(QString::fromUtf8("txtConsole"));
        txtConsole->setStyleSheet(QString::fromUtf8("background-color: rgb(0, 0, 0);\n"
"color: rgb(255, 255, 255);"));
        txtConsole->setReadOnly(true);

        verticalLayout->addWidget(txtConsole);

        LineCommander = new QLineEdit(TabConsole);
        LineCommander->setObjectName(QString::fromUtf8("LineCommander"));

        verticalLayout->addWidget(LineCommander);


        verticalLayout_3->addLayout(verticalLayout);

        Tabs->addTab(TabConsole, QString());
        TabBnet = new QWidget();
        TabBnet->setObjectName(QString::fromUtf8("TabBnet"));
        gridLayout_3 = new QGridLayout(TabBnet);
        gridLayout_3->setSpacing(6);
        gridLayout_3->setContentsMargins(11, 11, 11, 11);
        gridLayout_3->setObjectName(QString::fromUtf8("gridLayout_3"));
        BnetLayout = new QHBoxLayout();
        BnetLayout->setSpacing(6);
        BnetLayout->setObjectName(QString::fromUtf8("BnetLayout"));
        LineTextLayout = new QVBoxLayout();
        LineTextLayout->setSpacing(6);
        LineTextLayout->setObjectName(QString::fromUtf8("LineTextLayout"));
        txtBnet = new QTextEdit(TabBnet);
        txtBnet->setObjectName(QString::fromUtf8("txtBnet"));
        txtBnet->setStyleSheet(QString::fromUtf8("background-color: rgb(0, 0, 0);"));
        txtBnet->setReadOnly(true);

        LineTextLayout->addWidget(txtBnet);

        LineBnet = new QLineEdit(TabBnet);
        LineBnet->setObjectName(QString::fromUtf8("LineBnet"));

        LineTextLayout->addWidget(LineBnet);


        BnetLayout->addLayout(LineTextLayout);

        ListUsers = new QListWidget(TabBnet);
        ListUsers->setObjectName(QString::fromUtf8("ListUsers"));
        ListUsers->setMaximumSize(QSize(141, 16777215));

        BnetLayout->addWidget(ListUsers);


        gridLayout_3->addLayout(BnetLayout, 0, 0, 1, 1);

        Tabs->addTab(TabBnet, QString());

        gridLayout->addWidget(Tabs, 0, 0, 1, 1);

        MainForm->setCentralWidget(centralWidget);
        QWidget::setTabOrder(Tabs, txtConsole);
        QWidget::setTabOrder(txtConsole, LineCommander);
        QWidget::setTabOrder(LineCommander, txtBnet);
        QWidget::setTabOrder(txtBnet, LineBnet);
        QWidget::setTabOrder(LineBnet, ListUsers);

        retranslateUi(MainForm);

        Tabs->setCurrentIndex(0);


        QMetaObject::connectSlotsByName(MainForm);
    } // setupUi

    void retranslateUi(QMainWindow *MainForm)
    {
        MainForm->setWindowTitle(QApplication::translate("MainForm", "MainForm", 0, QApplication::UnicodeUTF8));
        Tabs->setTabText(Tabs->indexOf(TabConsole), QApplication::translate("MainForm", "Console", 0, QApplication::UnicodeUTF8));
        Tabs->setTabText(Tabs->indexOf(TabBnet), QApplication::translate("MainForm", "Battle.Net", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class MainForm: public Ui_MainForm {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINFORM_H
