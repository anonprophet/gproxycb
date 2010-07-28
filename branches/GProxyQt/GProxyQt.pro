# -------------------------------------------------
# Project created by QtCreator 2010-07-25T22:21:03
# -------------------------------------------------
QT += network \
    phonon
TARGET = GProxyQt
TEMPLATE = app
SOURCES += main.cpp \
    mainform.cpp \
    libhelper/helper.cpp \
    libgproxy/gpsprotocol.cpp \
    libgproxy/gproxy.cpp \
    libgproxy/gameprotocol.cpp \
    libgproxy/config.cpp \
    libgproxy/commandpacket.cpp \
    libgproxy/bnetprotocol.cpp \
    libgproxy/bnet.cpp \
    libgproxy/bncsutilinterface.cpp
HEADERS += mainform.h \
    includes.h \
    libhelper/helper.h \
    libgproxy/gpsprotocol.h \
    libgproxy/gproxy.h \
    libgproxy/gameprotocol.h \
    libgproxy/config.h \
    libgproxy/commandpacket.h \
    libgproxy/bnetprotocol.h \
    libgproxy/bnet.h \
    libgproxy/bncsutilinterface.h
FORMS += mainform.ui
OTHER_FILES += 
