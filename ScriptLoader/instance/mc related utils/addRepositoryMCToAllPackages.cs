addRepositoryMCToAllPackages
	
	"self new addRepositoryMCToAllPackages"
	
	MCWorkingCopy allManagers do: [:each | 
		each repositoryGroup
			 addRepository: self repositoryMC
			].
	
	