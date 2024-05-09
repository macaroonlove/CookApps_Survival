게임 타이틀: CookApps Survival
=============

## 1. 게임 시작 시, 시작 패널 보여줍니다.
>![StartPanel](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/2f216769-8b93-44eb-94cf-c677a4387701)

## 2. 시작 패널을 클릭하면, 패널이 사라지고 4명의 아군 유닛이 생성되며 슬롯이 추가됩니다.
>![InGame](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/699d3898-5b02-4470-9591-a70ea005da9d)
>▶ 탱커, 근거리 딜러, 원거리 딜러, 힐러 순으로 캐릭터 배치
>▶ 좌측 상단에 골드 및 상점여는 버튼   
>▶ 우측 상단에 배속(x1, x2, x3, x4) 버튼, 실행·중지 토글, 설정여는 버튼   
>▶ 좌측 하단에 유닛의 레벨, 이름, 공격력, 최대체력, 공격 및 스킬 사용 가능 여부(쿨타임), 경험치를 알려주는 슬롯
><br/>
>※ 추후에 필요하다면 아군 유닛을 추가하여 멤버를 교체할 수 있도록 Party Setting ScriptableObject에서 수정할 수 있습니다.
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/45c85856-3a3f-45b6-89d3-80e1dc9527cc)

## 3. 카메라는 1번 슬롯의 캐릭터를 추적하며, 1번 슬롯의 캐릭터가 죽었을 경우, 2 → 3 → 4번 유닛 순서대로 추적합니다.   
### ※추적하는 유닛을 메인 유닛으로 불러올 수 있도록 구현하였습니다.
>![Target](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/25efbcbe-8f03-462e-8eb8-d7033690d532)

## 4. 몬스터는 일정 주기로 캐릭터의 근처에서 생성되며, 추적 사거리보다 멀리 있을 경우 주변 위치를 순찰합니다.
>![Patrol](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/4600135e-e002-479e-b040-66fbe41a654a)

## 5. 추적 사거리 내에 메인 유닛이 존재할 경우, 추적합니다.
>![Tracking](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/d0ece86f-2d7c-45d8-adab-c9b1c56f032b)

## 6. 공격 사거리 내에 목표 타겟이 들어왔을 경우, 공격합니다.
>### 캐릭터의 경우
>>기본 공격과 스킬을 혼합하여 사용합니다.   
>>스킬을 사용이 가능하다면 스킬을, 그렇지 않다면 기본 공격을 사용합니다.   
>>※ 사용 가능 여부는 쿨타임으로 슬롯에서 보여집니다.   
>>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/edfd7351-9efb-4fa0-af51-b417825ba51a)
>>> #### 스킬
>>> 기절의 일격(탱커): 1m 범위 내의 단일 적에게 공격력 100%만큼 데미지를 입히며 1초간 기절시킵니다.   
>>> 단검 던지기(근거리 딜러): 2m 범위 내의 모든 적들에게 공격력의 100%만큼 데미지를 입힙니다.   
>>> 크리티컬 샷(원거리 딜러): 4m 범위 내의 단일 적에게 공격력 250%만큼 데미지를 입힙니다.   
>>> 치유의 기도(힐러): 4m 범위 내의 단일 아군의 체력을 공격력 250%만큼 회복킵니다.   
>>> ![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/abed7cb7-837b-49c4-a71b-863ceba5e6ae)
>>> ※ 해당 스킬 아이콘은 leonardo.ai를 사용하여 제작하였습니다.

>### 몬스터의 경우
>>기본 공격만 사용합니다.

## 7. 유닛 처치 시
>### 아군 유닛이 사망했을 경우
>일정 시간이 지나면 부활합니다.   
>부활 대기시간은 Party Setting ScriptableObject에서 수정이 가능합니다.   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/c3bf7bd4-6500-4f56-b3bd-95b2595dd182)
><br/>   
>부활 전 모든 유닛이 사망하는 경우 게임에 패배합니다.   
>![Defeat](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/4d5be92c-a98f-4e90-aad3-ab5447a6d936)

>### 적군 유닛이 사망했을 경우
>오브젝트 풀링으로 관리됩니다.   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/859adf7d-fcc8-4b18-86b5-656bfce66f00)
><br/>
>일정 이상의 적 유닛을 처치할 경우, 모든 일반 유닛들이 사망하고 보스 유닛이 나타납니다.   
>보스 유닛은 일반 적 유닛보다 크기가 크며 강합니다.
>보스 유닛을 물리치면 다음 스테이지로 넘어갑니다.
>![Boss](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/1f136139-7ed1-4f8f-9342-da9fd95c75ee)
><br/>
>적 유닛이 사망하면 일정량의 경험치와 골드를 얻을 수 있습니다.   
>Enemy ScriptableObject에서 유닛별로 처치 보상을 설정할 수 있습니다.   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/176eb32f-d714-4733-bf26-2b729fb19dd4)
><br/>



