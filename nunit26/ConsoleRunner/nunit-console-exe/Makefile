thisdir = nunit26/ConsoleRunner/nunit-console-exe
SUBDIRS = 
include ../../../build/rules.make

PROGRAM = $(topdir)/class/lib/$(PROFILE)/nunit-console.exe
LOCAL_MCS_FLAGS = \
	/r:nunit.framework.dll /r:nunit.util.dll /r:nunit.core.dll \
	/r:nunit-console-runner.dll
EXTRA_DISTFILES = App.ico \
	nunit-console.exe.csproj

include ../../../build/executable.make
