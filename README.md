![header](https://capsule-render.vercel.app/api?type=waving&height=300&color=FFB6C1&text=Level16%20%2t&fontColor=F0FFF0)

## [목차](#목차)

• [팀원 소개](#팀원-소개)  

• [기술 스택](#기술-스택)  

• [게임 조작법](#게임-소개)  

• [GameManager](#게임-매니저)  

• [Player](#플레이어)  

• [Weapon](#무기)  

• [Click Events](#클릭-이벤트)  

• [Stages](#스테이지)

<br/><br/><br/><br/><br/>

## 팀원 소개  
[목차로 돌아가기](#목차)

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/kookin09">
        <img src="https://avatars.githubusercontent.com/kookin09" width="100"/><br/>
        🔗 <sub><b>kookin09</b></sub>
      </a>
      <div style="min-height:60px;">
        <p>팀장입니다.<br/>팀원을 자주 치킨 바꿔 먹습니다.</p>
      </div>
    </td>
    <td align="center">
      <a href="https://github.com/unity9Parkjaehyun">
        <img src="https://avatars.githubusercontent.com/unity9Parkjaehyun" width="100"/><br/>
        🔗 <sub><b>unity9Parkjaehyun</b></sub>
      </a>
      <div style="min-height:60px;">
        <p>유쾌하게 가보자잇<br/>시원시원하게 가보자</p>
      </div>
    </td>
    <td align="center">
      <a href="https://github.com/ChunBae20">
        <img src="https://avatars.githubusercontent.com/ChunBae20" width="100"/><br/>
        🔗 <sub><b>ChunBae20</b></sub>
      </a>
      <div style="min-height:60px;">
        <p>말하는 감져임미다...ㅠㅠ</p>
      </div>
    </td>
    <td align="center">
      <a href="https://github.com/DH-C1">
        <img src="https://avatars.githubusercontent.com/DH-C1" width="100"/><br/>
        🔗 <sub><b>DH-C1</b></sub>
      </a>
      <div style="min-height:60px;">
        <p>월탱<br/>같이하실분?.</p>
      </div>
    </td>
    <td align="center">
      <a href="https://github.com/sunyeji">
        <img src="https://avatars.githubusercontent.com/sunyeji" width="100"/><br/>
        🔗 <sub><b>sunyeji</b></sub>
      </a>
      <div style="min-height:60px;">
        <p>땅콩이와<br/>싱글벙글 유니티생활</p>
      </div>
    </td>
  </tr>
</table>

<br/><br/><br/><br/><br/>

## 기술 스택  
[목차로 돌아가기](#목차)

![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)
<img src="https://github.com/user-attachments/assets/c7f5fece-6a4c-41fc-b51a-908d627984af" width="200" style="margin-top:-10px;" />

<br/><br/><br/><br/><br/>

## 게임 소개  
[목차로 돌아가기](#목차)

<table>
  <tr>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/d23abfc7-01dc-419d-9d7b-1e8bdcde3a6b" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>메인 기능 진입 애니메이션</b><br/>UI 버튼 클릭 시 연출됩니다</p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/826fa0eb-6a09-49c6-a9c9-fc31262ea309" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>골드 획득 애니메이션</b><br/>DOTween으로 부드럽게 표시</p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/da117c24-d0e9-4119-99f9-7ff376f9a799" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>오브젝트 풀링</b><br/>코인 생성과 회수 과정입니다</p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/28d85e29-f871-44b9-a8ff-642ea259a863" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>펫 1</b><br/>
        총 클릭횟수에 따라 펫이 진화를 합니다.<br/>
        펫이 진화를 하면 플레이어를 도와 같이 공격해줍니다.
      </p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/5db026e0-ddbf-45d7-8bde-fa098d8dfd4e" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>펫 2</b></p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/711a2a18-3e72-4e82-9e5f-aaef15bdd289" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>스탯 업그레이드</b><br/>
        몬스터와 전투하여 얻은 골드로<br/>
        플레이어 스탯을 업그레이드합니다.
      </p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/37baf81e-8ad4-42a2-91fc-51975e133eba" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>무기 시스템</b><br/>
        무기를 장착하거나<br/>
        강화를 할 수 있습니다.
      </p>
    </td>
  </tr>
</table>




<table>
  <tr>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/d23abfc7-01dc-419d-9d7b-1e8bdcde3a6b" width="220"/><br/>
      <p><b>메인 기능 진입 애니메이션</b><br/>UI 버튼 클릭 시 연출됩니다</p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/826fa0eb-6a09-49c6-a9c9-fc31262ea309" width="220"/><br/>
      <p><b>골드 획득 애니메이션</b><br/>DOTween으로 부드럽게 표시</p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/da117c24-d0e9-4119-99f9-7ff376f9a799" width="220"/><br/>
      <p><b>오브젝트 풀링</b><br/>코인 생성과 회수 과정입니다</p>
    </td>
  </tr>
</table>


<table>
  <tr>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/28d85e29-f871-44b9-a8ff-642ea259a863" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>펫 1</b><br/>
        총 클릭횟수에 따라 펫이 진화를 합니다.<br/>
        펫이 진화를 하면 플레이어를 도와 같이 공격해줍니다.
      </p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/5db026e0-ddbf-45d7-8bde-fa098d8dfd4e" style="width: 220px; max-height: 150px; object-fit: contain;" /><br/>
      <p><b>귀여운펫을 진화시켜 보세요..! </b></p>
    </td>
  </tr>
</table>


<table>
  <tr>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/711a2a18-3e72-4e82-9e5f-aaef15bdd289" width="220"/><br/>
      <p><b>스탯 업그레이드</b><br/>
        몬스터와 전투하여 얻은 골드로<br/>
        플레이어 스탯을 업그레이드합니다.
      </p>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/37baf81e-8ad4-42a2-91fc-51975e133eba" width="220"/><br/>
      <p><b>무기 시스템</b><br/>
        무기를 장착하거나<br/>
        강화를 할 수 있습니다.
      </p>
    </td>
  </tr>
</table>






<br/><br/><br/><br/><br/>

## 게임 매니저  
[목차로 돌아가기](#목차)

<br/><br/><br/><br/><br/>

## 플레이어
[목차로 돌아가기](#목차)

<br/><br/><br/><br/><br/>

## 무기  
[목차로 돌아가기](#목차)

<br/><br/><br/><br/><br/>

## 클릭 이벤트  
[목차로 돌아가기](#목차)

<br/><br/><br/><br/><br/>

## 스테이지  
[목차로 돌아가기](#목차)
