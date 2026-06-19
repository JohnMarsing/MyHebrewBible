namespace PWA.Features.Bible.Enums;

public record UserSettingsDTO(bool IsStandaloneAlephTavDetailsOn, 
															bool IsParashaDividerDetailsOn, 
															bool IsHebrewWordNumbersOn, // ToDo: rename to more generic IsVerseNumbersOn or IsShowWordCountOn
															bool IsVerseScrollButtonsOn);	

