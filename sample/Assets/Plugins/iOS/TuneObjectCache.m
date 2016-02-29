// Copyright 2014 Tune Inc. All Rights Reserved.

#import <Foundation/Foundation.h>

#import "TuneObjectCache.h"


@implementation TuneObjectCache

+ (instancetype)sharedInstance {
  static TuneObjectCache *sharedInstance;
  static dispatch_once_t onceToken;
  dispatch_once(&onceToken, ^{ sharedInstance = [[self alloc] init]; });
  return sharedInstance;
}

- (id)init {
  self = [super init];
  if (self) {
    _references = [[NSMutableDictionary alloc] init];
  }
  return self;
}

#if !__has_feature(objc_arc)
- (void)dealloc {
  [_references release];
  [super dealloc];
}
#endif

@end


@implementation NSObject (TuneOwnershipAdditions)

- (NSString *)Tune_referenceKey {
    return [NSString stringWithFormat:@"%p", (void *)self];
}

@end
