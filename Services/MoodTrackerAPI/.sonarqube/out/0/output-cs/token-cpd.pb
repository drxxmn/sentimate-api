‚2
PC:\Users\Sen\Documents\1_Fontys\sentimate-api\Services\MoodTrackerAPI\Program.cs
var		 
builder		 
=		 
WebApplication		 
.		 
CreateBuilder		 *
(		* +
args		+ /
)		/ 0
;		0 1
builder 
. 
Services 
. 
	AddScoped 
< 
MongoDbContext )
>) *
(* +
provider+ 3
=>4 6
{ 
var 
configuration 
= 
provider  
.  !
GetRequiredService! 3
<3 4
IConfiguration4 B
>B C
(C D
)D E
;E F
return 

new 
MongoDbContext 
( 
configuration +
)+ ,
;, -
} 
) 
; 
builder 
. 
Services 
. 
AddCors 
( 
options  
=>! #
{ 
options 
. 
	AddPolicy 
( 
$str +
,+ ,
builder 
=> 
builder 
. 
WithOrigins 
( 
$str 7
)7 8
. 
AllowAnyHeader 
( 
) 
. 
AllowAnyMethod 
( 
) 
. 
AllowCredentials 
( 
) 
)  
;  !
} 
) 
; 
using 
var 	
tracerProvider
 
= 
Sdk 
. '
CreateTracerProviderBuilder :
(: ;
); <
. 

UseGrafana 
( 
) 
.   
Build   

(  
 
)   
;   
using!! 
var!! 	
meterProvider!!
 
=!! 
Sdk!! 
.!! &
CreateMeterProviderBuilder!! 8
(!!8 9
)!!9 :
."" 

UseGrafana"" 
("" 
)"" 
.## 
Build## 

(##
 
)## 
;## 
using$$ 
var$$ 	
loggerFactory$$
 
=$$ 
LoggerFactory$$ '
.$$' (
Create$$( .
($$. /
builder$$/ 6
=>$$7 9
{%% 
builder&& 
.&& 
AddOpenTelemetry&& 
(&& 
logging&& $
=>&&% '
{'' 
logging(( 
.(( 

UseGrafana(( 
((( 
)(( 
;(( 
})) 
))) 
;)) 
}** 
)** 
;** 
builder.. 
... 
Services.. 
... 
AddAuthentication.. "
(.." #
JwtBearerDefaults..# 4
...4 5 
AuthenticationScheme..5 I
)..I J
.// 
AddJwtBearer// 
(// 
options// 
=>// 
{00 
var11 
auth0Settings11 
=11 
builder11 #
.11# $
Configuration11$ 1
.111 2

GetSection112 <
(11< =
$str11= D
)11D E
;11E F
options22 
.22 
	Authority22 
=22 
$"22 
$str22 &
{22& '
auth0Settings22' 4
[224 5
$str225 =
]22= >
}22> ?
"22? @
;22@ A
options33 
.33 
Audience33 
=33 
auth0Settings33 (
[33( )
$str33) 3
]333 4
;334 5
options44 
.44 %
TokenValidationParameters44 )
=44* +
new44, /%
TokenValidationParameters440 I
{55 	
ValidIssuer66 
=66 
$"66 
$str66 $
{66$ %
auth0Settings66% 2
[662 3
$str663 ;
]66; <
}66< =
"66= >
,66> ?
ValidAudience77 
=77 
auth0Settings77 )
[77) *
$str77* 4
]774 5
}88 	
;88	 

}99 
)99 
;99 
builder;; 
.;; 
Services;; 
.;; 
AddControllers;; 
(;;  
);;  !
;;;! "
builder<< 
.<< 
Services<< 
.<< #
AddEndpointsApiExplorer<< (
(<<( )
)<<) *
;<<* +
builder== 
.== 
Services== 
.== 
AddSwaggerGen== 
(== 
)==  
;==  !
var?? 
app?? 
=?? 	
builder??
 
.?? 
Build?? 
(?? 
)?? 
;?? 
ifBB 
(BB 
appBB 
.BB 
EnvironmentBB 
.BB 
IsDevelopmentBB !
(BB! "
)BB" #
)BB# $
{CC 
appDD 
.DD %
UseDeveloperExceptionPageDD !
(DD! "
)DD" #
;DD# $
appEE 
.EE 

UseSwaggerEE 
(EE 
)EE 
;EE 
appFF 
.FF 
UseSwaggerUIFF 
(FF 
)FF 
;FF 
}GG 
elseHH 
{II 
appJJ 
.JJ 
UseExceptionHandlerJJ 
(JJ 
$strJJ )
)JJ) *
;JJ* +
appKK 
.KK 
UseHstsKK 
(KK 
)KK 
;KK 
}LL 
appNN 
.NN 
UseHttpsRedirectionNN 
(NN 
)NN 
;NN 
appOO 
.OO 
UseStaticFilesOO 
(OO 
)OO 
;OO 
appPP 
.PP 

UseRoutingPP 
(PP 
)PP 
;PP 
appRR 
.RR 
UseCorsRR 
(RR 
$strRR !
)RR! "
;RR" #
appTT 
.TT 
UseAuthenticationTT 
(TT 
)TT 
;TT 
appUU 
.UU 
UseAuthorizationUU 
(UU 
)UU 
;UU 
appVV 
.VV 
MapControllersVV 
(VV 
)VV 
;VV 
appWW 
.WW 
RunWW 
(WW 
)WW 	
;WW	 
÷

YC:\Users\Sen\Documents\1_Fontys\sentimate-api\Services\MoodTrackerAPI\Models\MoodEntry.cs
	namespace 	
MoodTrackingService
 
. 
Models $
{ 
public 

class 
	MoodEntry 
{ 
[ 	
BsonId	 
] 
[		 	
BsonRepresentation			 
(		 
BsonType		 $
.		$ %
ObjectId		% -
)		- .
]		. /
public

 
string

 
Id

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
=

' (
string

) /
.

/ 0
Empty

0 5
;

5 6
public 
int 
Mood 
{ 
get 
; 
set "
;" #
}$ %
public 
string 
UserId 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
string- 3
.3 4
Empty4 9
;9 :
public 
DateTime 
	Timestamp !
{" #
get$ '
;' (
set) ,
;, -
}. /
=0 1
DateTime2 :
.: ;
UtcNow; A
;A B
} 
} √
]C:\Users\Sen\Documents\1_Fontys\sentimate-api\Services\MoodTrackerAPI\DTOs\MoodResponseDto.cs
	namespace 	
MoodTrackingService
 
. 
DTOs "
{ 
public 

class  
MoodEntryResponseDto %
{ 
public 
string 
Id 
{ 
get 
; 
set  #
;# $
}% &
=' (
string) /
./ 0
Empty0 5
;5 6
public 
int 
Mood 
{ 
get 
; 
set "
;" #
}$ %
public 
string 
UserId 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
string- 3
.3 4
Empty4 9
;9 :
public 
DateTime 
	Timestamp !
{" #
get$ '
;' (
set) ,
;, -
}. /
}		 
}

 ª
`C:\Users\Sen\Documents\1_Fontys\sentimate-api\Services\MoodTrackerAPI\DTOs\MoodEntryCreateDto.cs
	namespace 	
MoodTrackingService
 
. 
DTOs "
{ 
public 

class 
MoodEntryCreateDto #
{ 
public 
int 
Mood 
{ 
get 
; 
set "
;" #
}$ %
public 
string 
UserId 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
string- 3
.3 4
Empty4 9
;9 :
} 
} è
\C:\Users\Sen\Documents\1_Fontys\sentimate-api\Services\MoodTrackerAPI\Data\MongoDbContext.cs
	namespace 	
MoodTrackingService
 
. 
Data "
{ 
public 

class 
MongoDbContext 
:  !
IMongoDbContext" 1
{ 
private		 
readonly		 
IMongoDatabase		 '
	_database		( 1
;		1 2
public 
MongoDbContext 
( 
IConfiguration ,
configuration- :
): ;
{ 	
var 
client 
= 
new 
MongoClient (
(( )
configuration) 6
.6 7
GetValue7 ?
<? @
string@ F
>F G
(G H
$strH b
)b c
)c d
;d e
	_database 
= 
client 
. 
GetDatabase *
(* +
configuration+ 8
.8 9
GetValue9 A
<A B
stringB H
>H I
(I J
$strJ `
)` a
)a b
;b c
} 	
public 
IMongoCollection 
<  
	MoodEntry  )
>) *
MoodEntries+ 6
=>7 9
	_database: C
.C D
GetCollectionD Q
<Q R
	MoodEntryR [
>[ \
(\ ]
$str] j
)j k
;k l
} 
} ‡
]C:\Users\Sen\Documents\1_Fontys\sentimate-api\Services\MoodTrackerAPI\Data\IMongoDbContext.cs
	namespace 	
MoodTrackingService
 
. 
Data "
{ 
public 

	interface 
IMongoDbContext $
{ 
IMongoCollection 
< 
	MoodEntry "
>" #
MoodEntries$ /
{0 1
get2 5
;5 6
}7 8
}		 
}

 „Z
cC:\Users\Sen\Documents\1_Fontys\sentimate-api\Services\MoodTrackerAPI\Controllers\MoodController.cs
	namespace 	
MoodTrackingService
 
. 
Controllers )
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
MoodController 
:  !
ControllerBase" 0
{ 
private 
readonly 
MongoDbContext '
_context( 0
;0 1
public 
MoodController 
( 
MongoDbContext ,
context- 4
)4 5
{ 	
_context 
= 
context 
; 
} 	
private 
string 
GetUserIdFromToken )
() *
)* +
{ 	
var 

authHeader 
= 
HttpContext (
.( )
Request) 0
.0 1
Headers1 8
[8 9
$str9 H
]H I
.I J
ToStringJ R
(R S
)S T
;T U
if 
( 

authHeader 
. 

StartsWith %
(% &
$str& /
)/ 0
)0 1
{ 
var 
token 
= 

authHeader &
.& '
	Substring' 0
(0 1
$str1 :
.: ;
Length; A
)A B
.B C
TrimC G
(G H
)H I
;I J
var   
handler   
=   
new   !#
JwtSecurityTokenHandler  " 9
(  9 :
)  : ;
;  ; <
var!! 
	jsonToken!! 
=!! 
handler!!  '
.!!' (
	ReadToken!!( 1
(!!1 2
token!!2 7
)!!7 8
as!!9 ;
JwtSecurityToken!!< L
;!!L M
var"" 
userId"" 
="" 
	jsonToken"" &
?""& '
.""' (
Claims""( .
."". /
First""/ 4
(""4 5
claim""5 :
=>""; =
claim""> C
.""C D
Type""D H
==""I K
$str""L Q
)""Q R
?""R S
.""S T
Value""T Y
;""Y Z
return## 
userId## 
;## 
}$$ 
return%% 
null%% 
;%% 
}&& 	
[(( 	
HttpGet((	 
](( 
[)) 	
	Authorize))	 
])) 
public** 
async** 
Task** 
<** 
ActionResult** &
<**& '
IEnumerable**' 2
<**2 3 
MoodEntryResponseDto**3 G
>**G H
>**H I
>**I J
Get**K N
(**N O
)**O P
{++ 	
var,, 
userId,, 
=,, 
GetUserIdFromToken,, +
(,,+ ,
),,, -
;,,- .
if-- 
(-- 
userId-- 
==-- 
null-- 
)-- 
{.. 
return// 
Unauthorized// #
(//# $
)//$ %
;//% &
}00 
var22 
entries22 
=22 
await22 
_context22  (
.22( )
MoodEntries22) 4
.224 5
Find225 9
(229 :
entry22: ?
=>22@ B
entry22C H
.22H I
UserId22I O
==22P R
userId22S Y
)22Y Z
.22Z [
ToListAsync22[ f
(22f g
)22g h
;22h i
var33 
responseDtos33 
=33 
entries33 &
.33& '

ConvertAll33' 1
(331 2
entry332 7
=>338 :
new33; > 
MoodEntryResponseDto33? S
{44 
Id55 
=55 
entry55 
.55 
Id55 
,55 
Mood66 
=66 
entry66 
.66 
Mood66 !
,66! "
	Timestamp77 
=77 
entry77 !
.77! "
	Timestamp77" +
,77+ ,
UserId88 
=88 
entry88 
.88 
UserId88 %
}99 
)99 
;99 
return:: 
Ok:: 
(:: 
responseDtos:: "
)::" #
;::# $
};; 	
[== 	
HttpPost==	 
]== 
[>> 	
	Authorize>>	 
]>> 
public?? 
async?? 
Task?? 
<?? 
IActionResult?? '
>??' (
Post??) -
(??- .
[??. /
FromBody??/ 7
]??7 8
MoodEntryCreateDto??9 K
dto??L O
)??O P
{@@ 	
ifAA 
(AA 
!AA 

ModelStateAA 
.AA 
IsValidAA #
)AA# $
{BB 
returnCC 

BadRequestCC !
(CC! "

ModelStateCC" ,
)CC, -
;CC- .
}DD 
varFF 
userIdFF 
=FF 
GetUserIdFromTokenFF +
(FF+ ,
)FF, -
;FF- .
ifGG 
(GG 
userIdGG 
==GG 
nullGG 
)GG 
{HH 
returnII 
UnauthorizedII #
(II# $
)II$ %
;II% &
}JJ 
varLL 
userTimezoneOffsetLL "
=LL# $
HttpContextLL% 0
.LL0 1
RequestLL1 8
.LL8 9
HeadersLL9 @
[LL@ A
$strLLA R
]LLR S
;LLS T
ifNN 
(NN 
!NN 
intNN 
.NN 
TryParseNN 
(NN 
userTimezoneOffsetNN 0
,NN0 1
outNN2 5
intNN6 9
offsetMinutesNN: G
)NNG H
)NNH I
{OO 
returnPP 

BadRequestPP !
(PP! "
$strPP" <
)PP< =
;PP= >
}QQ 
varSS 
currentDateTimeUtcSS "
=SS# $
DateTimeSS% -
.SS- .
UtcNowSS. 4
;SS4 5
varTT 
userCurrentDateTimeTT #
=TT$ %
currentDateTimeUtcTT& 8
.TT8 9

AddMinutesTT9 C
(TTC D
offsetMinutesTTD Q
)TTQ R
;TTR S
varUU 
userCurrentDateUU 
=UU  !
userCurrentDateTimeUU" 5
.UU5 6
DateUU6 :
;UU: ;
varWW 
filterWW 
=WW 
BuildersWW !
<WW! "
	MoodEntryWW" +
>WW+ ,
.WW, -
FilterWW- 3
.WW3 4
WhereWW4 9
(WW9 :
entryWW: ?
=>WW@ B
entryWWC H
.WWH I
UserIdWWI O
==WWP R
userIdWWS Y
&&WWZ \
entryWW] b
.WWb c
	TimestampWWc l
>=WWm o
userCurrentDateWWp 
&&
WWÄ Ç
entry
WWÉ à
.
WWà â
	Timestamp
WWâ í
<
WWì î
userCurrentDate
WWï §
.
WW§ •
AddDays
WW• ¨
(
WW¨ ≠
$num
WW≠ Æ
)
WWÆ Ø
)
WWØ ∞
;
WW∞ ±
varXX 
existingEntryXX 
=XX 
awaitXX  %
_contextXX& .
.XX. /
MoodEntriesXX/ :
.XX: ;
FindXX; ?
(XX? @
filterXX@ F
)XXF G
.XXG H
FirstOrDefaultAsyncXXH [
(XX[ \
)XX\ ]
;XX] ^
ifZZ 
(ZZ 
existingEntryZZ 
!=ZZ  
nullZZ! %
)ZZ% &
{[[ 
var\\ 
update\\ 
=\\ 
Builders\\ %
<\\% &
	MoodEntry\\& /
>\\/ 0
.\\0 1
Update\\1 7
.\\7 8
Set\\8 ;
(\\; <
entry\\< A
=>\\B D
entry\\E J
.\\J K
Mood\\K O
,\\O P
dto\\Q T
.\\T U
Mood\\U Y
)\\Y Z
.\\Z [
Set\\[ ^
(\\^ _
entry\\_ d
=>\\e g
entry\\h m
.\\m n
	Timestamp\\n w
,\\w x
currentDateTimeUtc	\\y ã
)
\\ã å
;
\\å ç
await]] 
_context]] 
.]] 
MoodEntries]] *
.]]* +
UpdateOneAsync]]+ 9
(]]9 :
filter]]: @
,]]@ A
update]]B H
)]]H I
;]]I J
}^^ 
else__ 
{`` 
varaa 
entryaa 
=aa 
newaa 
	MoodEntryaa  )
{bb 
Moodcc 
=cc 
dtocc 
.cc 
Moodcc #
,cc# $
UserIddd 
=dd 
userIddd #
,dd# $
	Timestampee 
=ee 
currentDateTimeUtcee  2
}ff 
;ff 
awaithh 
_contexthh 
.hh 
MoodEntrieshh *
.hh* +
InsertOneAsynchh+ 9
(hh9 :
entryhh: ?
)hh? @
;hh@ A
}ii 
returnkk 
	NoContentkk 
(kk 
)kk 
;kk 
}ll 	
[nn 	

HttpDeletenn	 
(nn 
$strnn 
)nn 
]nn 
[oo 	
	Authorizeoo	 
]oo 
publicpp 
asyncpp 
Taskpp 
<pp 
IActionResultpp '
>pp' (
Deletepp) /
(pp/ 0
stringpp0 6
idpp7 9
)pp9 :
{qq 	
varrr 
userIdrr 
=rr 
GetUserIdFromTokenrr +
(rr+ ,
)rr, -
;rr- .
ifss 
(ss 
userIdss 
==ss 
nullss 
)ss 
{tt 
returnuu 
Unauthorizeduu #
(uu# $
)uu$ %
;uu% &
}vv 
varxx 
entryxx 
=xx 
awaitxx 
_contextxx &
.xx& '
MoodEntriesxx' 2
.xx2 3
Findxx3 7
(xx7 8
entryxx8 =
=>xx> @
entryxxA F
.xxF G
IdxxG I
==xxJ L
idxxM O
&&xxP R
entryxxS X
.xxX Y
UserIdxxY _
==xx` b
userIdxxc i
)xxi j
.xxj k
FirstOrDefaultAsyncxxk ~
(xx~ 
)	xx Ä
;
xxÄ Å
ifyy 
(yy 
entryyy 
==yy 
nullyy 
)yy 
{zz 
return{{ 
NotFound{{ 
({{  
){{  !
;{{! "
}|| 
var~~ 
result~~ 
=~~ 
await~~ 
_context~~ '
.~~' (
MoodEntries~~( 3
.~~3 4
DeleteOneAsync~~4 B
(~~B C
entry~~C H
=>~~I K
entry~~L Q
.~~Q R
Id~~R T
==~~U W
id~~X Z
&&~~[ ]
entry~~^ c
.~~c d
UserId~~d j
==~~k m
userId~~n t
)~~t u
;~~u v
if 
( 
result 
. 
DeletedCount #
==$ &
$num' (
)( )
{
ÄÄ 
return
ÅÅ 
NotFound
ÅÅ 
(
ÅÅ  
)
ÅÅ  !
;
ÅÅ! "
}
ÇÇ 
return
ÑÑ 
	NoContent
ÑÑ 
(
ÑÑ 
)
ÑÑ 
;
ÑÑ 
}
ÖÖ 	
}
ÜÜ 
}áá 