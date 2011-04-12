thisdir = nunit26/NUnitFixtures/fixtures
SUBDIRS = 
include ../../../build/rules.make

LIBRARY = nunit.fixtures.dll
LIBRARY_SNK = $(topdir)/nunit26/nunit.snk

LOCAL_MCS_FLAGS= \
	-r:nunit.core.dll -r:nunit.core.interfaces.dll -r:nunit.util.dll \
	-r:System.dll -r:System.Xml.dll -r:System.Runtime.Remoting.dll \
	/d:MONO /d:StronglyNamedAssembly
NO_TEST = yo

RESX_RES = 

EXTRA_DISTFILES = \
	nunit.fixtures.dll.csproj	\
	$(RESX_RES:.resources=.resx)

CLEAN_FILES = $(RESX_RES)

ifneq (net_2_0, $(PROFILE))
NO_INSTALL = yes
install-local: install-symlink
uninstall-local: uninstall-symlink
endif

include ../../../build/library.make

$(the_lib): $(RESX_RES)

$(RESX_RES): %.resources: %.resx
	$(RESGEN) `echo $< | $(PLATFORM_CHANGE_SEPARATOR_CMD)`

symlinkdir = $(mono_libdir)/mono/$(FRAMEWORK_VERSION)
install-symlink:
	$(MKINSTALLDIRS) $(DESTDIR)$(symlinkdir)
	cd $(DESTDIR)$(symlinkdir) && rm -f $(LIBRARY_NAME) && ln -s ../2.0/$(LIBRARY_NAME) $(LIBRARY_NAME)

uninstall-symlink:
	rm -f $(DESTDIR)$(symlinkdir)/$(LIBRARY_NAME)
