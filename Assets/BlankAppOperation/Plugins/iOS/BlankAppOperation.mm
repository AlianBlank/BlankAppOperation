#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


extern "C" {
    bool CanOpenURL(char* urlStr);
    void OpenURL(char* urlStr);
}


bool CanOpenURL(char* urlStr)
{
    NSURL *url = [NSURL URLWithString:[NSString stringWithUTF8String:urlStr]];
    return [[UIApplication sharedApplication] canOpenURL:url];
}

void OpenURL(char* urlStr)
{
    NSURL *url = [NSURL URLWithString:[NSString stringWithUTF8String:urlStr]];
    [[UIApplication sharedApplication] openURL:url];
}