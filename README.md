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
>### ※추적하는 유닛을 메인 유닛으로 불러올 수 있도록 구현하였습니다.
>![Target](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/25efbcbe-8f03-462e-8eb8-d7033690d532)

## 4. 몬스터는 일정 주기로 캐릭터의 근처에서 생성되며, 추적 사거리보다 멀리 있을 경우 주변 위치를 순찰합니다.
>![Patrol](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/4600135e-e002-479e-b040-66fbe41a654a)

## 5. 추적 사거리 내에 메인 유닛이 존재할 경우, 추적합니다.
>추적은 Unity의 AI Navigation 2.0을 사용하여 구현하였습니다.   
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
>적 유닛이 사망하면 적에게 가한 데미지에 비례하여 일정량의 경험치와 골드를 얻을 수 있습니다.   
>Enemy ScriptableObject에서 유닛별로 처치 보상을 설정할 수 있습니다.   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/176eb32f-d714-4733-bf26-2b729fb19dd4)   

## 상점   
>획득한 골드를 통해 상점에서 스탯 증가 포션, 회복 포션 및 스킬 강화를 구매할 수 있습니다.   
>![Store](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/6e5a42d8-0ed4-4d8e-a642-1b0a305ce264)   

## 스테이지
> 스테이지는 초원, 숲, 독 숲, 사막, 지옥의 컨셉으로 구현하였습니다.   
> 각 스테이지마다 나오는 몬스터와 보스가 다르게 하였으며 스탯을 다르게 구현하였습니다.   
> 각 스테이지마다 다른 BGM이 나오게 됩니다.(출처: Suno AI)   
> ![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/a273f5ea-4810-42e2-9ad7-9b1ecbc1c630)

## 설정
>사운드 설정 및 게임 종료를 할 수 있습니다.   
>![Setting](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/dd2388d4-725d-4c30-9166-ec85308002bb)

## 구조 (BattleManager, Unit)


## ※ 데이터 관리(ScriptableObject)   
>아군과 적군의 데이터   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/0997b987-33bc-4258-b37a-363c0da260e4)   
><br/>   
>아군 유닛의 스킬   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/0429d206-be5a-4c3b-ac2a-f2a1d20a0b9c)   
><br/>   
>파티 설정 및 적군 스포너   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/38cf31dd-a917-4f3f-b28c-033496ecfbe8)   
><br/>
>상태 이상 관리   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/6828f082-6b82-4651-9d8b-71a153e33687)   
><br/>
>상점 아이템   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/77fa4246-e4ac-4cca-a59b-5fe1c57116ea)   
><br/>   
>스테이지 관리   
> ![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/5a7001c6-5251-41bb-aecc-407da4b1439b)   
><br/>
>FX관리: 파티클, 사운드 실행 템플릿   
>![image](https://github.com/macaroonlove/CookApps_PxP/assets/87137181/357d86bc-f2b5-44b9-aeb3-24a8bfc8725c)   




