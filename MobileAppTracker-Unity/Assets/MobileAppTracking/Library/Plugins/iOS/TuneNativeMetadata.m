// Copyright 2014 Tune Inc. All Rights Reserved.

#import <Foundation/Foundation.h>

#import "TuneNativeMetadata.h"


@implementation TuneNativeMetadata

- (id)init {
    self = [super init];
    if (self) {
        _keywords = [[NSMutableArray alloc] init];
        _customTargets = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (void)addKeyword:(NSString *)keyword {
    [self.keywords addObject:keyword];
}

- (void)setBirthDateWithMonth:(NSInteger)month day:(NSInteger)day year:(NSInteger)year {
#if __has_feature(objc_arc)
    NSDateComponents *components = [[NSDateComponents alloc] init];
#else
    NSDateComponents *components = [[[NSDateComponents alloc] init] autorelease];
#endif
    components.month = month;
    components.day = day;
    components.year = year;
    NSCalendar *gregorian = [[NSCalendar alloc] initWithCalendarIdentifier:NSGregorianCalendar];
    self.birthDate = [gregorian dateFromComponents:components];
    NSLog(@"setBirthDateWithMonth: m = %ld, d = %ld, y = %ld, date = %@", (long)month, (long)day, (long)year, self.birthDate);
#if __has_feature(objc_arc)
    gregorian = nil;
#else
    [gregorian release], gregorian = nil;
#endif
}

- (void)setGenderWithCode:(TuneAdGender)gender {
    switch (gender) {
        case kTuneAdGenderMale:
            self.gender = TuneGenderMale;
            break;
        case kTuneAdGenderFemale:
            self.gender = TuneGenderFemale;
            break;
        default:
            self.gender = TuneGenderUnknown;
            break;
    }
}

- (void)setCustomTargetWithKey:(NSString *)key value:(NSString *)value {
    [self.customTargets setValue:value forKey:key];
}

- (TuneAdMetadata *)metadata {
    TuneAdMetadata *meta = [[TuneAdMetadata alloc] init];
    meta.keywords = self.keywords;
    meta.customTargets = self.customTargets;
    meta.birthDate = self.birthDate;
    meta.gender = self.gender;
    meta.latitude = self.latitude;
    meta.longitude = self.longitude;
    meta.altitude = self.altitude;
    meta.debugMode = self.debugMode;
    
#if __has_feature(objc_arc)
    return meta;
#else
    return [meta autorelease];
#endif
}

@end
