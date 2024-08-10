////////////////////////////////////////////////////////////////////
//
// C:\Users\mahra\Documents\Visual Studio 2010\Projects\2dgamepad tester\2dgamepad tester\2dgamepad tester\LiveFiles\Title.spa.h
//
// Auto-generated on Saturday, 10 August 2024 at 14:47:42
// Xbox LIVE Game Config project version 1.0.1.0
// SPA Compiler version 1.0.0.0
//
////////////////////////////////////////////////////////////////////

#ifndef __GAMEPAD_36STER_SPA_H__
#define __GAMEPAD_36STER_SPA_H__

#ifdef __cplusplus
extern "C" {
#endif

//
// Title info
//

#define TITLEID_GAMEPAD_36STER                      0x08888880

//
// Context ids
//
// These values are passed as the dwContextId to XUserSetContext.
//


//
// Context values
//
// These values are passed as the dwContextValue to XUserSetContext.
//

// Values for X_CONTEXT_PRESENCE

#define CONTEXT_PRESENCE_PRESENCEMODE1              0

// Values for X_CONTEXT_GAME_MODE

#define CONTEXT_GAME_MODE_GAMEMODE1                 0

//
// Property ids
//
// These values are passed as the dwPropertyId value to XUserSetProperty
// and as the dwPropertyId value in the XUSER_PROPERTY structure.
//

#define PROPERTY_XNAVIRTUALTITLEIDPART1             0x10000001
#define PROPERTY_XNAVIRTUALTITLEIDPART2             0x10000002
#define PROPERTY_XNAVIRTUALTITLEIDPART3             0x10000007
#define PROPERTY_XNAVIRTUALTITLEIDPART4             0x10000008
#define PROPERTY_XNASESSIONISJOINABLE               0x10000009
#define PROPERTY_XNAALLOWHOSTMIGRATION              0x1000001A
#define PROPERTY_XNAFRAMEWORKVERSION                0x1000001B
#define PROPERTY_XNAHOSTGAMERTAGPART1               0x20000003
#define PROPERTY_XNAHOSTGAMERTAGPART2               0x20000004
#define PROPERTY_XNACUSTOMSESSIONPROPERTY1          0x2000000A
#define PROPERTY_XNACUSTOMSESSIONPROPERTY2          0x2000000B
#define PROPERTY_XNACUSTOMSESSIONPROPERTY3          0x2000000C
#define PROPERTY_XNACUSTOMSESSIONPROPERTY4          0x2000000D
#define PROPERTY_XNACUSTOMSESSIONPROPERTY5          0x2000000E
#define PROPERTY_XNACUSTOMSESSIONPROPERTY6          0x2000000F
#define PROPERTY_XNACUSTOMSESSIONPROPERTY7          0x20000010
#define PROPERTY_XNACUSTOMSESSIONPROPERTY8          0x20000011
#define PROPERTY_RATING                             0x2000001C

//
// Achievement ids
//
// These values are used in the dwAchievementId member of the
// XUSER_ACHIEVEMENT structure that is used with
// XUserWriteAchievements and XUserCreateAchievementEnumerator.
//

#define ACHIEVEMENT_ACHIEVEMENT01                   1
#define ACHIEVEMENT_ACHIEVEMENT02                   2
#define ACHIEVEMENT_ACHIEVEMENT03                   3
#define ACHIEVEMENT_ACHIEVEMENT04                   4
#define ACHIEVEMENT_ACHIEVEMENT05                   5
#define ACHIEVEMENT_ACHIEVEMENT06                   6
#define ACHIEVEMENT_ACHIEVEMENT07                   7
#define ACHIEVEMENT_ACHIEVEMENT08                   8
#define ACHIEVEMENT_ACHIEVEMENT09                   9
#define ACHIEVEMENT_ACHIEVEMENT10                   10
#define ACHIEVEMENT_ACHIEVEMENT11                   11
#define ACHIEVEMENT_ACHIEVEMENT12                   12
#define ACHIEVEMENT_ACHIEVEMENT13                   13
#define ACHIEVEMENT_ACHIEVEMENT14                   14
#define ACHIEVEMENT_ACHIEVEMENT15                   15
#define ACHIEVEMENT_ACHIEVEMENT16                   16
#define ACHIEVEMENT_ACHIEVEMENT17                   17
#define ACHIEVEMENT_ACHIEVEMENT18                   18
#define ACHIEVEMENT_ACHIEVEMENT19                   19
#define ACHIEVEMENT_ACHIEVEMENT20                   20

//
// AvatarAssetAward ids
//


//
// Stats view ids
//
// These are used in the dwViewId member of the XUSER_STATS_SPEC structure
// passed to the XUserReadStats* and XUserCreateStatsEnumerator* functions.
//

// Skill leaderboards for ranked game modes

#define STATS_VIEW_SKILL_RANKED_GAMEMODE1           0xFFFF0000

// Skill leaderboards for unranked (standard) game modes

#define STATS_VIEW_SKILL_STANDARD_GAMEMODE1         0xFFFE0000

// Title defined leaderboards

#define STATS_VIEW_OVERALLLEADERBOARD               1

//
// Stats view column ids
//
// These ids are used to read columns of stats views.  They are specified in
// the rgwColumnIds array of the XUSER_STATS_SPEC structure.  Rank, rating
// and gamertag are not retrieved as custom columns and so are not included
// in the following definitions.  They can be retrieved from each row's
// header (e.g., pStatsResults->pViews[x].pRows[y].dwRank, etc.).
//

// Column ids for OVERALLLEADERBOARD


//
// Matchmaking queries
//
// These values are passed as the dwProcedureIndex parameter to
// XSessionSearch to indicate which matchmaking query to run.
//

#define SESSION_MATCH_QUERY_FINDSESSIONS            0

//
// Gamer pictures
//
// These ids are passed as the dwPictureId parameter to XUserAwardGamerTile.
//


//
// Strings
//
// These ids are passed as the dwStringId parameter to XReadStringsFromSpaFile.
//

#define SPASTRING_XNAEMPTYSTRING                    37
#define SPASTRING_GAMEMODE1                         39
#define SPASTRING_PRESENCEMODE1                     40
#define SPASTRING_ACHIEVEMENT01NAME                 41
#define SPASTRING_ACHIEVEMENT01DESCRIPTION          42
#define SPASTRING_ACHIEVEMENT01HOWTO                43
#define SPASTRING_ACHIEVEMENT02NAME                 44
#define SPASTRING_ACHIEVEMENT02DESCRIPTION          45
#define SPASTRING_ACHIEVEMENT02HOWTO                46
#define SPASTRING_ACHIEVEMENT03NAME                 47
#define SPASTRING_ACHIEVEMENT03DESCRIPTION          48
#define SPASTRING_ACHIEVEMENT03HOWTO                49
#define SPASTRING_ACHIEVEMENT04NAME                 50
#define SPASTRING_ACHIEVEMENT04DESCRIPTION          51
#define SPASTRING_ACHIEVEMENT04HOWTO                52
#define SPASTRING_ACHIEVEMENT05NAME                 53
#define SPASTRING_ACHIEVEMENT05DESCRIPTION          54
#define SPASTRING_ACHIEVEMENT05HOWTO                55
#define SPASTRING_ACHIEVEMENT06NAME                 56
#define SPASTRING_ACHIEVEMENT06DESCRIPTION          57
#define SPASTRING_ACHIEVEMENT06HOWTO                58
#define SPASTRING_ACHIEVEMENT07NAME                 59
#define SPASTRING_ACHIEVEMENT07DESCRIPTION          60
#define SPASTRING_ACHIEVEMENT07HOWTO                61
#define SPASTRING_ACHIEVEMENT08NAME                 62
#define SPASTRING_ACHIEVEMENT08DESCRIPTION          63
#define SPASTRING_ACHIEVEMENT08HOWTO                64
#define SPASTRING_ACHIEVEMENT09NAME                 65
#define SPASTRING_ACHIEVEMENT09DESCRIPTION          66
#define SPASTRING_ACHIEVEMENT09HOWTO                67
#define SPASTRING_ACHIEVEMENT10NAME                 68
#define SPASTRING_ACHIEVEMENT10DESCRIPTION          69
#define SPASTRING_ACHIEVEMENT10HOWTO                70
#define SPASTRING_ACHIEVEMENT11NAME                 71
#define SPASTRING_ACHIEVEMENT11DESCRIPTION          72
#define SPASTRING_ACHIEVEMENT11HOWTO                73
#define SPASTRING_ACHIEVEMENT12NAME                 74
#define SPASTRING_ACHIEVEMENT12DESCRIPTION          75
#define SPASTRING_ACHIEVEMENT12HOWTO                76
#define SPASTRING_OVERALLLEADERBOARD                77
#define SPASTRING_OVERALLLEADERBOARDRATING          78
#define SPASTRING_RATING                            79
#define SPASTRING_XNAEMPTYSTRING_1                  80
#define SPASTRING_XNAEMPTYSTRING_2                  81
#define SPASTRING_XNAEMPTYSTRING_3                  82
#define SPASTRING_XNAEMPTYSTRING_4                  83
#define SPASTRING_XNAEMPTYSTRING_5                  84
#define SPASTRING_XNAEMPTYSTRING_6                  85
#define SPASTRING_XNAEMPTYSTRING_7                  86
#define SPASTRING_XNAEMPTYSTRING_8                  87
#define SPASTRING_XNAEMPTYSTRING_9                  88
#define SPASTRING_XNAEMPTYSTRING_10                 89
#define SPASTRING_XNAEMPTYSTRING_11                 90
#define SPASTRING_XNAEMPTYSTRING_12                 91
#define SPASTRING_XNAEMPTYSTRING_13                 92
#define SPASTRING_XNAEMPTYSTRING_14                 93
#define SPASTRING_XNAEMPTYSTRING_15                 94
#define SPASTRING_XNAEMPTYSTRING_16                 95
#define SPASTRING_ACHIEVEMENT13HOWTO                96
#define SPASTRING_ACHIEVEMENT13NAME                 97
#define SPASTRING_ACHIEVEMENT13DESCRIPTION          98
#define SPASTRING_ACHIEVEMENT14HOWTO                99
#define SPASTRING_ACHIEVEMENT14NAME                 100
#define SPASTRING_ACHIEVEMENT14DESCRIPTION          101
#define SPASTRING_ACHIEVEMENT15HOWTO                102
#define SPASTRING_ACHIEVEMENT15NAME                 103
#define SPASTRING_ACHIEVEMENT15DESCRIPTION          104
#define SPASTRING_ACHIEVEMENT16HOWTO                105
#define SPASTRING_ACHIEVEMENT16NAME                 106
#define SPASTRING_ACHIEVEMENT16DESCRIPTION          107
#define SPASTRING_ACHIEVEMENT17HOWTO                108
#define SPASTRING_ACHIEVEMENT17NAME                 109
#define SPASTRING_ACHIEVEMENT17DESCRIPTION          110
#define SPASTRING_ACHIEVEMENT18HOWTO                111
#define SPASTRING_ACHIEVEMENT18NAME                 112
#define SPASTRING_ACHIEVEMENT18DESCRIPTION          113
#define SPASTRING_ACHIEVEMENT19HOWTO                114
#define SPASTRING_ACHIEVEMENT19NAME                 115
#define SPASTRING_ACHIEVEMENT19DESCRIPTION          116
#define SPASTRING_ACHIEVEMENT20HOWTO                117
#define SPASTRING_ACHIEVEMENT20NAME                 118
#define SPASTRING_ACHIEVEMENT20DESCRIPTION          119


#ifdef __cplusplus
}
#endif

#endif // __GAMEPAD_36STER_SPA_H__


