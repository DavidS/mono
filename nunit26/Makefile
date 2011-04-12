thisdir = nunit26
SUBDIRS = NUnitFramework/framework NUnitCore/interfaces NUnitCore/core \
	ClientUtilities/util NUnitMocks/mocks \
	ConsoleRunner/nunit-console ConsoleRunner/nunit-console-exe
include ../build/rules.make

DISTFILES = license.txt nunit.sln nunit.snk \
	nunit20under21.config nunit20under22.config nunit21under22.config \
	CommonAssemblyInfo.cs

all-local install-local csproj-local test-local run-test-local run-test-ondotnet-local clean-local uninstall-local doc-update-local:

dist-local: dist-default

