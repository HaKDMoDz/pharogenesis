setUp
	| package trivial1rel installed1rel installed2rel tricky2rel conf1 conf2 tricky3rel1 tricky3rel2 tricky1rel seaside httpview kom1 kom2 |
	map := SMSqueakMap new reload.
	goranAccount := map newAccount: 'Goran' username: 'Goran' email: 'g@g.com'.
	
	"Add a few packages to test with:
	
	Tricky1 1
		Installed1 1
		Tricky2 1
	Tricky2 1
		Installed1 1
		TrivialToInstall1 1
		Tricky3 1
	
		Installed1 1
		TrivialToInstall1 1
		Tricky3 2
	Tricky3 2
		TrivialToInstall1 1

		Installed2 1
	
	Seaside
		KomHttpServer 1
	
	HttpView
		KomHttpServer 2
	"
	{
		{'A'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 3}.
		{'B'. {'Squeak3.6'. 'Stable'}. 2}.
		{'TrivialToInstall1'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Installed1'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Installed2'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'AlreadyInstallable1'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Tricky1'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Tricky2'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Tricky3'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 2}.
		{'Circular1'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Circular2'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Circular3'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'Seaside'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
		{'KomHttpServer'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 2}.
		{'HttpView'. {'Squeak3.6'. 'Squeak3.7'. 'Stable'}. 1}.
	} do: [:arr |
			package := SMPackage newIn: map.
			package name: arr first.
			arr second do: [:cn | package addCategory: (map categoryWithNameBeginning: cn)].
			arr third timesRepeat: [package newRelease ].
			goranAccount addObject: package].
	
	trivial1rel := (map packageWithName: 'TrivialToInstall1') lastRelease.
	trivial1rel publisher: goranAccount.
	
	installed1rel := (map packageWithName: 'Installed1') lastRelease.
	installed1rel publisher: goranAccount; noteInstalled.
	installed2rel := (map packageWithName: 'Installed2') lastRelease.
	installed2rel publisher: goranAccount; noteInstalled.

	((map packageWithName: 'AlreadyInstallable1') lastRelease
		publisher: goranAccount;
		addConfiguration)
				addRequiredRelease: installed1rel.
	"Tricky1 has just a single configuration with one installed and one not installed."
	tricky1rel := (map packageWithName: 'Tricky1') lastRelease.
	tricky2rel := (map packageWithName: 'Tricky2') lastRelease.			
	(tricky1rel publisher: goranAccount; addConfiguration)
				addRequiredRelease: installed1rel; "already installed"
				addRequiredRelease: tricky2rel. "not installed"

	"Tricky2 has two configurations:
		1: an installed, a trivial one and Tricky3 r1.
		2: an installed, a trivial one and Tricky3 r2."
	conf1 := tricky2rel publisher: goranAccount; addConfiguration.
	conf2 := tricky2rel addConfiguration.
	
	tricky3rel1 := (map packageWithName: 'Tricky3') releases first.
	tricky3rel2 := (map packageWithName: 'Tricky3') lastRelease.
	tricky3rel1 publisher: goranAccount.
	tricky3rel2 publisher: goranAccount.

	conf1 addRequiredRelease: installed1rel; addRequiredRelease: trivial1rel; addRequiredRelease: tricky3rel1.
	conf2 addRequiredRelease: installed1rel; addRequiredRelease: trivial1rel; addRequiredRelease: tricky3rel2.
	
	"Tricky3rel2 has two configurations:
		1: trivial1
		2: installed2rel"
	conf1 := tricky3rel2 publisher: goranAccount; addConfiguration.
	conf2 := tricky3rel2 addConfiguration.
	conf1 addRequiredRelease: trivial1rel.
	conf2 addRequiredRelease: installed2rel.
	

	seaside := (map packageWithName: 'Seaside') lastRelease.
	seaside publisher: goranAccount.
	httpview := (map packageWithName: 'HttpView') lastRelease.
	httpview publisher: goranAccount.
	kom1 := (map packageWithName: 'KomHttpServer') firstRelease.
	kom1 publisher: goranAccount.
	kom2 := (map packageWithName: 'KomHttpServer') lastRelease.
	kom2 publisher: goranAccount.
	
	conf1 := seaside addConfiguration.
	conf2 := httpview addConfiguration.
	conf1 addRequiredRelease: kom1.
	conf2 addRequiredRelease: kom2