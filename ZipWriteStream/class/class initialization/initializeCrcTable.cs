initializeCrcTable
	"ZipWriteStream initialize"
	CrcTable := #(16r00000000 16r77073096 16rEE0E612C 16r990951BA 16r076DC419
  16r706AF48F 16rE963A535 16r9E6495A3 16r0EDB8832 16r79DCB8A4
  16rE0D5E91E 16r97D2D988 16r09B64C2B 16r7EB17CBD 16rE7B82D07
  16r90BF1D91 16r1DB71064 16r6AB020F2 16rF3B97148 16r84BE41DE
  16r1ADAD47D 16r6DDDE4EB 16rF4D4B551 16r83D385C7 16r136C9856
  16r646BA8C0 16rFD62F97A 16r8A65C9EC 16r14015C4F 16r63066CD9
  16rFA0F3D63 16r8D080DF5 16r3B6E20C8 16r4C69105E 16rD56041E4
  16rA2677172 16r3C03E4D1 16r4B04D447 16rD20D85FD 16rA50AB56B
  16r35B5A8FA 16r42B2986C 16rDBBBC9D6 16rACBCF940 16r32D86CE3
  16r45DF5C75 16rDCD60DCF 16rABD13D59 16r26D930AC 16r51DE003A
  16rC8D75180 16rBFD06116 16r21B4F4B5 16r56B3C423 16rCFBA9599
  16rB8BDA50F 16r2802B89E 16r5F058808 16rC60CD9B2 16rB10BE924
  16r2F6F7C87 16r58684C11 16rC1611DAB 16rB6662D3D 16r76DC4190
  16r01DB7106 16r98D220BC 16rEFD5102A 16r71B18589 16r06B6B51F
  16r9FBFE4A5 16rE8B8D433 16r7807C9A2 16r0F00F934 16r9609A88E
  16rE10E9818 16r7F6A0DBB 16r086D3D2D 16r91646C97 16rE6635C01
  16r6B6B51F4 16r1C6C6162 16r856530D8 16rF262004E 16r6C0695ED
  16r1B01A57B 16r8208F4C1 16rF50FC457 16r65B0D9C6 16r12B7E950
  16r8BBEB8EA 16rFCB9887C 16r62DD1DDF 16r15DA2D49 16r8CD37CF3
  16rFBD44C65 16r4DB26158 16r3AB551CE 16rA3BC0074 16rD4BB30E2
  16r4ADFA541 16r3DD895D7 16rA4D1C46D 16rD3D6F4FB 16r4369E96A
  16r346ED9FC 16rAD678846 16rDA60B8D0 16r44042D73 16r33031DE5
  16rAA0A4C5F 16rDD0D7CC9 16r5005713C 16r270241AA 16rBE0B1010
  16rC90C2086 16r5768B525 16r206F85B3 16rB966D409 16rCE61E49F
  16r5EDEF90E 16r29D9C998 16rB0D09822 16rC7D7A8B4 16r59B33D17
  16r2EB40D81 16rB7BD5C3B 16rC0BA6CAD 16rEDB88320 16r9ABFB3B6
  16r03B6E20C 16r74B1D29A 16rEAD54739 16r9DD277AF 16r04DB2615
  16r73DC1683 16rE3630B12 16r94643B84 16r0D6D6A3E 16r7A6A5AA8
  16rE40ECF0B 16r9309FF9D 16r0A00AE27 16r7D079EB1 16rF00F9344
  16r8708A3D2 16r1E01F268 16r6906C2FE 16rF762575D 16r806567CB
  16r196C3671 16r6E6B06E7 16rFED41B76 16r89D32BE0 16r10DA7A5A
  16r67DD4ACC 16rF9B9DF6F 16r8EBEEFF9 16r17B7BE43 16r60B08ED5
  16rD6D6A3E8 16rA1D1937E 16r38D8C2C4 16r4FDFF252 16rD1BB67F1
  16rA6BC5767 16r3FB506DD 16r48B2364B 16rD80D2BDA 16rAF0A1B4C
  16r36034AF6 16r41047A60 16rDF60EFC3 16rA867DF55 16r316E8EEF
  16r4669BE79 16rCB61B38C 16rBC66831A 16r256FD2A0 16r5268E236
  16rCC0C7795 16rBB0B4703 16r220216B9 16r5505262F 16rC5BA3BBE
  16rB2BD0B28 16r2BB45A92 16r5CB36A04 16rC2D7FFA7 16rB5D0CF31
  16r2CD99E8B 16r5BDEAE1D 16r9B64C2B0 16rEC63F226 16r756AA39C
  16r026D930A 16r9C0906A9 16rEB0E363F 16r72076785 16r05005713
  16r95BF4A82 16rE2B87A14 16r7BB12BAE 16r0CB61B38 16r92D28E9B
  16rE5D5BE0D 16r7CDCEFB7 16r0BDBDF21 16r86D3D2D4 16rF1D4E242
  16r68DDB3F8 16r1FDA836E 16r81BE16CD 16rF6B9265B 16r6FB077E1
  16r18B74777 16r88085AE6 16rFF0F6A70 16r66063BCA 16r11010B5C
  16r8F659EFF 16rF862AE69 16r616BFFD3 16r166CCF45 16rA00AE278
  16rD70DD2EE 16r4E048354 16r3903B3C2 16rA7672661 16rD06016F7
  16r4969474D 16r3E6E77DB 16rAED16A4A 16rD9D65ADC 16r40DF0B66
  16r37D83BF0 16rA9BCAE53 16rDEBB9EC5 16r47B2CF7F 16r30B5FFE9
  16rBDBDF21C 16rCABAC28A 16r53B39330 16r24B4A3A6 16rBAD03605
  16rCDD70693 16r54DE5729 16r23D967BF 16rB3667A2E 16rC4614AB8
  16r5D681B02 16r2A6F2B94 16rB40BBE37 16rC30C8EA1 16r5A05DF1B
  16r2D02EF8D
).