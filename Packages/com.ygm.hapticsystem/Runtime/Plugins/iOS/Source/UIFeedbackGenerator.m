#import <Foundation/Foundation.h>

UISelectionFeedbackGenerator* SelectionFeedbackGenerator;
UINotificationFeedbackGenerator* NotificationFeedbackGenerator;
UIImpactFeedbackGenerator* LightImpactFeedbackGenerator;
UIImpactFeedbackGenerator* MediumImpactFeedbackGenerator;
UIImpactFeedbackGenerator* HeavyImpactFeedbackGenerator;
UIImpactFeedbackGenerator* RigidImpactFeedbackGenerator;
UIImpactFeedbackGenerator* SoftImpactFeedbackGenerator;


void InstantiateFeedbackGenerators()
{
    SelectionFeedbackGenerator = [[UISelectionFeedbackGenerator alloc] init];
    NotificationFeedbackGenerator = [[UINotificationFeedbackGenerator alloc] init];
    LightImpactFeedbackGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleLight];
    MediumImpactFeedbackGenerator =  [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium];
    HeavyImpactFeedbackGenerator =  [[UIImpactFeedbackGenerator alloc] initWithStyle: UIImpactFeedbackStyleHeavy];
    if (@available(iOS 13, *))
    {
      RigidImpactFeedbackGenerator =  [[UIImpactFeedbackGenerator alloc] initWithStyle: UIImpactFeedbackStyleRigid];
    	SoftImpactFeedbackGenerator =  [[UIImpactFeedbackGenerator alloc] initWithStyle: UIImpactFeedbackStyleSoft];
    }
    else
    {
      RigidImpactFeedbackGenerator =  [[UIImpactFeedbackGenerator alloc] initWithStyle: UIImpactFeedbackStyleHeavy];
    	SoftImpactFeedbackGenerator =  [[UIImpactFeedbackGenerator alloc] initWithStyle: UIImpactFeedbackStyleLight];
    }
}

void ReleaseFeedbackGenerators ()
{
    SelectionFeedbackGenerator = nil;
    NotificationFeedbackGenerator = nil;
    LightImpactFeedbackGenerator = nil;
    MediumImpactFeedbackGenerator = nil;
    HeavyImpactFeedbackGenerator = nil;
    RigidImpactFeedbackGenerator = nil;
    SoftImpactFeedbackGenerator = nil;
}

void PrepareSelectionFeedbackGenerator()
{
    [SelectionFeedbackGenerator prepare];
}

void PrepareNotificationFeedbackGenerator()
{
    [NotificationFeedbackGenerator prepare];
}

void PrepareLightImpactFeedbackGenerator()
{
    [LightImpactFeedbackGenerator prepare];
}

void PrepareMediumImpactFeedbackGenerator()
{
    [MediumImpactFeedbackGenerator prepare];
}

void PrepareHeavyImpactFeedbackGenerator()
{
    [HeavyImpactFeedbackGenerator prepare];
}

void PrepareRigidImpactFeedbackGenerator()
{
    [RigidImpactFeedbackGenerator prepare];
}

void PrepareSoftImpactFeedbackGenerator()
{
    [SoftImpactFeedbackGenerator prepare];
}


void SelectionHaptic()
{
    [SelectionFeedbackGenerator prepare];
    [SelectionFeedbackGenerator selectionChanged];
}

void SuccessHaptic()
{
    [NotificationFeedbackGenerator prepare];
    [NotificationFeedbackGenerator notificationOccurred:UINotificationFeedbackTypeSuccess];
}

void WarningHaptic()
{
    [NotificationFeedbackGenerator prepare];
    [NotificationFeedbackGenerator notificationOccurred:UINotificationFeedbackTypeWarning];
}

void FailureHaptic()
{
    [NotificationFeedbackGenerator prepare];
    [NotificationFeedbackGenerator notificationOccurred:UINotificationFeedbackTypeError];
}

void LightImpactHaptic()
{
    [LightImpactFeedbackGenerator prepare];
    [LightImpactFeedbackGenerator impactOccurred];
}

void MediumImpactHaptic()
{
    [MediumImpactFeedbackGenerator prepare];
    [MediumImpactFeedbackGenerator impactOccurred];
}

void HeavyImpactHaptic()
{
    [HeavyImpactFeedbackGenerator prepare];
    [HeavyImpactFeedbackGenerator impactOccurred];
}

void RigidImpactHaptic()
{
    [RigidImpactFeedbackGenerator prepare];
    [RigidImpactFeedbackGenerator impactOccurred];
}

void SoftImpactHaptic()
{
    [SoftImpactFeedbackGenerator prepare];
    [SoftImpactFeedbackGenerator impactOccurred];
}
