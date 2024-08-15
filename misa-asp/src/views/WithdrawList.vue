<template>
  <div>
    <Header />
    <div class="body-payment">
      <div class="side-bar">
        <!-- SideBar -->
        <SideBar />
      </div>
      <div class="main-content">
        <!-- router list goes here -->
        <RouterList />

        <!-- Main content goes here -->
        <div class="main-container">
          <ButtonList
            @search="handleSearch"
            @filters-updated="updateFilters"
            :dateField="dateField"
          />
          <MSWithdrawList
            :searchQuery="searchQuery"
            :totalRecords="pageData.totalRecords"
            @updatePageData="updatePageData"
            @updateTotalRecords="updateTotalRecords"
            @sort-updated="updateSort"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Header from "../components/WithdrawList/Header.vue";
import SideBar from "../components/WithdrawList/SideBar.vue";
import ButtonList from "../components/WithdrawList/ButtonList.vue";
import RouterList from "../components/WithdrawList/RouterList.vue";
import MSWithdrawList from "../components/ControlComponent/MSWithdrawList.vue";

export default {
  name: "WithdrawList",
  components: {
    Header,
    SideBar,
    ButtonList,
    RouterList,
    MSWithdrawList,
  },

  data() {
    return {
      dateField: ["posted_date"],
      searchQuery: "",
      pageData: {
        filters: null,
        sort: null,
        currentPage: null,
        itemsPerPage: null,
      },
    };
  },
  methods: {
    handleSearch(query) {
      this.searchQuery = query;
    },
    updatePageData(pageData) {
      this.pageData = {
        ...this.pageData,
        ...pageData,
      };
    },
    updateTotalRecords(totalRecords) {
      this.pageData.totalRecords = totalRecords;
    },
    updateFilters(filters) {
      this.pageData.filters = filters;
    },
    updateSort(sort) {
      this.pageData.sort = sort;
    },
  },
};
</script>

<style scoped>
body {
  margin: 0;
  font-family: Arial, sans-serif;
}
.main-container {
  background-color: #fff;
  height: 90%;
}

header {
  width: 100%;
  background-color: #f8f8f8;
  padding: 10px 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  position: fixed;
  top: 0;
  z-index: 1000;
}

.body-payment {
  display: flex;
  margin-top: 40px;
}

.side-bar {
  background-color: #f8f8f8;
  margin-top: 10px;
  border-right: 1px solid #ddd;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
  background-color: #fff;
  width: 190px;
}

.main-content {
  width: 100%;
  padding-left: 20px;
  padding-top: 15px;
  overflow-y: auto;
  background-color: #eeeff1;
  padding-right: 10px;
}
</style>
