<template>
  <div class="vx-navbar-wrapper">
    <div class="vx-navbar navbar-custom flex items-center">
      <!---->
      <div class="logo-container logo-container-sidebar">
        <div class="custom-bg"></div>
        <a
          href="https://asp.misa.vn/app"
          class="menu-container-option z-i-20"
        ></a
        ><a href="/app" class="logo"
          ><img
            src="https://actaspcdng1.misacdn.net/assets/asp_logo_white-56488ac6.svg"
            class="img-icon" />
          <div class="custom-theme-logo"></div></a
        ><!---->
      </div>

      <div
        class="icon-three-stripes-position mi mi-24 mi-sidebar-three-stripes pointer z-i-20"
      ></div>
      <div class="flex header-branch">
        <div class="con-ms-tooltip">
          <div class="msTooltipParent"></div>
          <div class="tooltip-content">
            <div class="header-branch-name">
              CÔNG TY TNHH ĐÔNG PHƯƠNG ĐỒNG NAI VIỆT NAM-TEST-CHÍNH THỨC
            </div>
          </div>
        </div>
      </div>
      <!---->
      <div class="con-ms-tooltip">
        <div class="msTooltipParent"></div>
        <div class="tooltip-content">
          <div class="header-current-db">
            <div class="icon-header-current-db"></div>
            <div class="header-current-db-name">PCMINH_KETOAN_2016</div>
          </div>
        </div>
      </div>
      <div class="select-branch-work" style="display: none">
        <div class="flex m-row align-justify">
          <div class="w-full">
            <!----><!---->
            <div class="line"></div>
            <div class="m-row-footer">
              <button
                name="button"
                class="ms-component ms-button ms-button-size-default ms-button-secondary ms-button-secondary-disabled-false ms-button-radius-false ms-button"
              >
                <!---->
                <div class="ms-button-text ms-button--text flex align-center">
                  Hủy
                </div></button
              ><button
                name="button"
                class="ms-component ms-button footer-button-right ms-button-size-default ms-button-primary ms-button-primary-disabled-false ms-button-radius-false ms-button"
              >
                <!---->
                <div class="ms-button-text ms-button--text flex align-center">
                  Đồng ý
                </div>
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="ms-space"></div>
      <div class="flex header-quick-search">
        <div alt-shortkey-target="Y" class="hidden"></div>
        <form autocomplete="off">
          <div class="ms-input ms-editor ms-input--input-search">
            <!---->
            <div>
              <div class="ms-component ms-con-input-label ms-input style-label">
                <!----><span
                  ><div class="ms-con-input">
                    <input
                      placeholder="Nhập từ khóa tìm kiếm"
                      type="text"
                      alt-shortkey-target="X"
                      alt-shortkey-action="1"
                      class="ms-inputx ms-input--input normal hasIcon icon-after-input placehoder ms-input-normal"
                    /><!---->
                    <div class="con-ms-tooltip">
                      <div class="msTooltipParent">
                        <div
                          class="ms-tooltip"
                          style="
                            left: 0px;
                            top: 0px;
                            position: fixed;
                            display: none;
                          "
                        ></div>
                      </div>
                      <div class="tooltip-content">
                        <div
                          iconpack="material-icons"
                          icon="mi-search"
                          class="icon-inputx notranslate ms-input--icon icon-after mi-search mi mi-16"
                        ></div>
                      </div>
                    </div>
                    <!----><!---->
                  </div></span
                ><span></span>
              </div>
            </div>
          </div>
        </form>
        <div class="ms-search-component">
          <!---->
        </div>
      </div>
      <div class="ms-download">
        <div class="con-ms-tooltip">
          <div class="msTooltipParent"></div>
          <div class="tooltip-content">
            <div class="pointer header-menu-icon flex align-center">
              <div class="wrap-notify pt-0">
                <div class="mi mi-24 header-icon mi-install-new"></div>
                <!---->
              </div>
            </div>
          </div>
        </div>
        <!---->
      </div>
      <div class="con-ms-tooltip">
        <div class="msTooltipParent"></div>
        <div class="tooltip-content">
          <div class="header-menu-icon system-settings ms-dropdown">
            <div class="ms-button-text ms-button--text flex align-center">
              <!----><!----><a href.prevent="" class="flex items-center"
                ><div class="header-icon pointer mi mi-24 mi-setting-new"></div
              ></a>
            </div>
          </div>
        </div>
      </div>
      <div class="chatting">
        <div class="con-ms-tooltip">
          <div class="msTooltipParent"></div>
          <div class="tooltip-content">
            <div class="pointer flex header-menu-icon">
              <div
                class="mi mi-24 header-menu-icon-simple header-icon mi-message-new"
              ></div>
            </div>
          </div>
        </div>
        <div class="ms-message ms-component">
          <div class="message-icon-tray"><!----><!----></div>
          <!---->
        </div>
      </div>
      <!---->
      <div class="help-btn help-the-nav-bar"></div>
      <div class="notify">
        <div class="con-ms-tooltip header-menu-icon">
          <div class="msTooltipParent"></div>
          <div class="tooltip-content">
            <div class="pointer header-icon">
              <div class="wrap-notify header-menu-icon-simple">
                <div class="mi mi-24 mi-notice-new wiggle"></div>
                <!---->
              </div>
            </div>
          </div>
        </div>
        <div class="ms-notify"><!----></div>
      </div>
      <div class="con-ms-tooltip header-menu-icon">
        <div class="msTooltipParent"></div>
        <div class="tooltip-content">
          <div class="header-menu-icon ms-dropdown">
            <div class="ms-button-text ms-button--text flex align-center">
              <!----><!----><a href.prevent="" class="flex items-center"
                ><div class="header-icon pointer mi mi-24 mi-question-new"></div
              ></a>
            </div>
          </div>
        </div>
      </div>
      <div class="con-ms-tooltip">
        <div class="msTooltipParent"></div>
        <div class="tooltip-content">
          <div class="header-menu-icon ms-dropdown">
            <div class="ms-button-text ms-button--text flex align-center">
              <!----><!----><a href.prevent="" class="flex items-center"
                ><div class="header-icon mi mi-24 mi-category-new pointer"></div
              ></a>
            </div>
          </div>
        </div>
      </div>
      <div class="con-ms-tooltip">
        <div class="msTooltipParent"></div>
        <div class="tooltip-content">
          <div class="header-menu-icon system-settings ms-dropdown">
            <div class="ms-button-text ms-button--text flex align-center">
              <a href.prevent="" class="flex items-center"
                ><div
                  class="header-icon pointer mi mi-24 mi-useful-knowledge-new__nav"
                ></div
              ></a>
            </div>
          </div>
        </div>
      </div>
      <div class="account-info">
        <div class="dropdown-secondary ms-dropdown">
          <button name="button" shortkey-target="" class="ms-button-secondary">
            <!---->
            <div
              class="avater-infor ms-button-text ms-button--text flex align-center"
            >
              <div
                href.prevent=""
                class="flex items-center ms-dropdown-user avatar-weak-warning"
              >
                <div class="user-ava">
                  <div class="mi mi-32 mi-user-avatar avatar">
                    <img
                      src="https://id.misa.com.vn/image/getavatar?userId=97f41a49-cf43-4402-81c2-1c62c396bcc7&amp;width=32&amp;height=32"
                    />
                  </div>
                </div>
              </div>
              <!----><!---->
            </div>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "HeaderPaymentMaster",
  data() {
    return {};
  },
  methods: {},
};
</script>

<style scoped>
.vx-navbar-wrapper {
  left: 0;
  top: 0;
  position: absolute;
  width: 100%;
  background: linear-gradient(to right, #2ca01c, #00c1bf);
  z-index: 100;
  text-decoration-color: initial;
  font-family: inherit;
  font-size: inherit;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}
.navbar-sticky .vx-navbar-wrapper .vx-navbar {
  padding: 0;
  height: var(--nav-bar-height);
}
.items-center {
  align-items: center !important;
}

.flex {
  display: flex !important;
}

.mi {
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat;
  cursor: pointer;
}
.z-i-20 {
  z-index: 20;
}
.logo {
  height: 24px;
  display: block;
  z-index: 1;
}
.menu-container-option {
  width: 24px;
  height: 24px;
  margin-right: 10px;
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat -424px -88px;
  z-index: 1;
}
.logo-container {
  display: flex;
  padding: 12px;
  margin-right: 25px;
  align-items: center;
  justify-content: center;
  justify-content: flex-start;
  position: relative;
}
.mi-24 {
  width: 24px;
  height: 24px;
  min-width: 24px;
  min-height: 24px;
}
.mi-sidebar-three-stripes {
  background-position: -312px -32px;
}
.pointer {
  cursor: pointer;
}
.icon-three-stripes-position {
  margin: 0 0 0 -12px;
}
.z-i-20 {
  z-index: 20;
}
.header-branch {
  cursor: pointer;
}
.con-ms-tooltip .tooltip-content {
  height: 100%;
}
.header-branch-name {
  padding-left: 5px;
  overflow: hidden;
  white-space: nowrap;
  font-weight: 400;
  max-width: 450px;
  text-overflow: ellipsis;
  transition: all ease 0.2s;
  font-size: 14px;
  color: #fff;
  padding-top: 3px;
}
@media (max-width: 900px) {
  .header-branch-name {
    max-width: 80px;
  }
}
@media (max-width: 1300px) {
  .header-branch-name {
    max-width: 100px;
  }
}
@media (max-width: 1366px) {
  .header-branch-name {
    max-width: 150px;
  }
}
.header-current-db {
  display: flex;
  align-items: center;
  background: rgba(239, 239, 239, 0.2);
  padding: 0 12px;
  border-radius: 40px;
  margin-left: 20px;
  font-size: 13px;
  cursor: pointer;
  height: 30px;
  margin-right: 10px;
}
.icon-header-current-db {
  width: 8px;
  height: 8px;
  background-color: #85ff74;
  border-radius: 50%;
}
.header-current-db-name {
  padding-left: 7px;
  max-width: 200px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  transition: all ease 0.2s;
  color: #fff;
}
@media (max-width: 900px) {
  .header-current-db-name {
    max-width: 0;
    padding-left: 0;
  }
}
@media (max-width: 1300px) {
  .header-current-db-name {
    max-width: 80px;
  }
}
.select-branch-work {
  width: 400px;
  border: 1px solid #babec5;
  background: #fff;
  color: #212111;
  position: absolute;
  top: 40px;
  left: 30px;
  border-radius: 3px;
}
.select-branch-work .m-row {
  margin: 0 20px 20px;
}
.align-justify {
  justify-content: space-between;
}
.m-row:last-of-type {
  padding-bottom: 0;
}
.m-row {
  padding-bottom: var(--row-spacing);
}
.w-full {
  width: 100% !important;
}
.line {
  margin-top: 20px;
  border-top: 1px solid #d4d7dc;
}
.select-branch-work .m-row .m-row-footer {
  margin-top: 20px;
  display: flex;
  justify-content: space-between;
}
.ms-space {
  padding: 0;
  margin: 0;
  flex-grow: 1 !important;
  background: 0 0;
}
.flex.header-quick-search {
  position: relative;
}
.header-quick-search {
  margin-right: 20px;
  width: 200px;
}
.ms-input {
  color: #111;
  width: 100%;
}
.hidden {
  display: none !important;
}
.ms-con-input-label {
  position: relative;
  width: 100%;
}
.ms-con-input-label .ms-con-input {
  display: flex;
  align-items: flex-start;
  flex-direction: column;
  position: relative;
  justify-content: center;
}
.ms-input .ms-input--input.hasIcon.icon-after-input {
  padding-left: 10px;
  outline: none;
}
.header-quick-search .ms-input .ms-input--input {
  background: transparent;
  border-radius: 3px;
  color: #fffc;
  border: 1px solid rgba(235, 237, 240, 0.8);
}
.ms-input--input.icon-after-input {
  padding-right: 2.75rem;
}
input::placeholder {
  color: #fff;
  font-style: italic;
}
.ms-input--input.hasIcon {
  padding-left: 32px;
}
.ms-input--input.normal,
.ms-input--input.large {
  padding: 5px 12px 5px 10px;
}
.ms-input--input {
  font-size: 13px;
  height: var(--input-height);
  color: inherit;
  position: relative;
  padding: 0 10px;
  border-radius: 2px;
  border: 1px solid #babec5;
  box-sizing: border-box;
  width: 100%;
}
.ms-input-normal {
  width: 100%;
  height: 100%;
  background-color: #fff;
}
.ms-tooltip {
  display: block;
  height: auto;
  position: absolute;
  z-index: 30000;
  background: #505050;
  padding: 3px 10px;
  font-size: 12px;
  border-radius: 2px;
  white-space: nowrap;
  color: #fff;
}
.header-quick-search .ms-input .ms-con-input .ms-input--icon {
  color: #fffc;
}
.ms-input--icon.icon-after {
  left: auto;
  right: 12px;
  border-right: 0;
}
.ms-input--icon {
  cursor: pointer;
  position: absolute;
  left: 5px;
  font-size: 1.1rem;
  border-right: 1px solid rgba(0, 0, 0, 0.1);
  padding-right: 3px;
  color: #0006;
  -webkit-user-select: none;
  -moz-user-select: none;
  user-select: none;
  top: 6px;
}
.mi-search {
  background-position: -992px -360px;
}
.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}
.header-menu-icon {
  position: relative;
  height: 48px;
  width: 44px;
}
.pointer {
  cursor: pointer;
}
.align-center {
  justify-content: center;
  align-items: center;
}

.wrap-notify {
  position: relative;
  padding-top: 12px;
  margin-top: 0;
}
.pt-0 {
  padding-top: 0 !important;
}
.header-icon {
  margin: 0 10px;
}
.mi-install-new,
.mi-install-new--hover {
  background-position: -1771px -28px;
}
.mi-24 {
  width: 24px;
  height: 24px;
  min-width: 24px;
  min-height: 24px;
}
.ms-dropdown {
  display: flex;
}
.ms-radius-false {
  border-radius: 3px 0 0 3px;
}
.ms-button-radius-false {
  border-radius: 3px;
}
.ms-button {
  transition: all 0.2s ease;
  border: 0;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  color: #fff;
  box-sizing: border-box;
  background: 0 0;
  overflow: visible;
}
.width-100-percent {
  width: 100%;
}
.ms-button .ms-button--text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
}
.ms-con-dropdown-radius-false-false {
  border-radius: 0 3px 3px 0;
}
.expand-more-button {
  height: 100%;
  position: relative;
}
.ms-button-radius-false {
  border-radius: 3px;
}
.avater-infor {
  padding-right: 10px;
}
.ms-button {
  transition: all 0.2s ease;
  border: 0;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  color: #fff;
  box-sizing: border-box;
  background: 0 0;
  overflow: visible;
}
.width-100-percent {
  width: 100%;
}
.ms-button .ms-button--text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
}
.items-center {
  align-items: center !important;
}
.mi-setting-new,
.mi-setting-new--hover {
  background-position: -1796px -28px;
}
.header-icon {
  margin: 0 10px;
}
.chatting {
  position: relative;
}
.header-menu-icon-simple {
  margin-top: 12px;
}
.mi-message-new,
.mi-message-new--hover {
  background-position: -1821px -28px;
}
.wrap-notify {
  position: relative;
  padding-top: 12px;
  margin-top: 0;
}
.wiggle[data-v-06e91a04] {
  animation: none;
}
.mi-notice-new,
.mi-notice-new--hover {
  background-position: -1847px -28px;
}
.mi-question-new,
.mi-question-new--hover {
  background-position: -1870px -28px;
}
.mi-category-new,
.mi-category-new--hover {
  background-position: -1893px -28px;
}
.mi-useful-knowledge-new__nav {
  background-position: -1916px -27px;
  height: 28px;
}
.account-info:hover {
  cursor: pointer;
  background-color: #fff3;
}
.account-info {
  height: 48px;
  width: 54px;
  align-items: center;
  justify-content: center;
  display: flex;
}
.avatar-weak-warning {
  border-radius: 50%;
  border: 2px solid #f45d48 !important;
}
.ms-button .ms-button--text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
}
.dropdown-secondary
  .ms-button-secondary
  .ms-button-text
  .ms-dropdown-user
  .user-ava {
  background: #e2e2e2;
  border-radius: 15px;
}
.ms-button-secondary {
  border: unset;
  color: unset;
  background: unset;
}

.mi-32 {
  width: 32px;
  height: 32px;
  min-width: 32px;
  min-height: 32px;
}
.user-ava .avatar img {
  border-radius: 50%;
  background-color: transparent;
}
</style>
